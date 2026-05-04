using JuniorPharon.Models;
using JuniorPharon.Repository;
using JuniorPharon.ViewModels;
using LinqKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utilities;


namespace JuniorPharon.Services

{
    
    public class AccountService
    {
        private readonly UnitOfWork _unitOfWork;
        IConfiguration appSettingConfiguration;
        private readonly ILogger<AccountService> _logger;


        public AccountService(UnitOfWork unitOfWork, IConfiguration appSettingConfiguration , ILogger<AccountService> logger)
        {
            _unitOfWork = unitOfWork;
            this.appSettingConfiguration = appSettingConfiguration;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateAccount(UserRegisterVM user)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                if (await IsEmailTaken(user.Email))
                {
                    return ServiceResult.FailureResult(
                        "الايميل المدخل تم استخدامه من قبل.",
                        HttpStatusCode.BadRequest
                    );
                }

                if (user.ImageFile != null)
                {
                    user.ProfileImg = await UploadMedia.AddImageAsync(user.ImageFile);
                }

                var userRes = await _unitOfWork._userRepository.Register(user);

                if (!userRes.Succeeded)
                {
                    var errors = string.Join(", ", userRes.Errors.Select(e => e.Description));
                    return ServiceResult.FailureResult(errors, HttpStatusCode.BadRequest);
                }

                var currentUser = await _unitOfWork._userRepository.FindByEmailAsync(user.Email);

                if (user.Role == "Admin")
                {
                    await _unitOfWork._adminRepository.AddAsync(new Admin
                    {
                        UserId = currentUser.Id
                    });
                }
                else if (user.Role == "Client")
                {
                    await _unitOfWork._clientRepository.AddAsync(new Client
                    {
                        UserId = currentUser.Id
                    });
                }
               

                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();

                return ServiceResult.SuccessResult("Account created successfully.");
            }
            //catch
            //{
            //    await transaction.RollbackAsync();
            //    return ServiceResult.FailureResult(
            //        "حدث خطأ أثناء إنشاء الحساب.",
            //        HttpStatusCode.InternalServerError
            //    );
            //}


            // the best choise : 

            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError(ex, "Error while creating account");

                return ServiceResult.FailureResult(
                    "حدث خطأ أثناء إنشاء الحساب.",
                    HttpStatusCode.InternalServerError
                );
            }

            //catch (Exception ex)
            //{
            //    await transaction.RollbackAsync();

            //    return ServiceResult.FailureResult(
            //        ex.Message,   // 👈 هنا بقى بيرجع الخطأ الحقيقي
            //        HttpStatusCode.InternalServerError
            //    );
            //}
        }


        public async Task<bool> IsEmailTaken(string email)
        {
            var user = await _unitOfWork._userRepository.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _unitOfWork._userRepository.FindById(userId);
        }

        //public async Task<SignInResult> Login(UserLoginVM user)
        //{
        //    return await _unitOfWork._userRepository.Login(user);
        //}

        public async Task<AuthResponse> LoginWithToken(UserLoginVM user)
        {
            var res = await _unitOfWork._userRepository.Login(user);

            if (!res.Succeeded)
                return null;

            var currentUser = await _unitOfWork._userRepository
                .FindByEmailAsync(user.UserNameOrEmail);

            var roles = await _unitOfWork._userRepository
                .GetUserRoles(currentUser);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, currentUser.UserName),
        new Claim(ClaimTypes.Email, currentUser.Email),
        new Claim(ClaimTypes.NameIdentifier, currentUser.Id)
    };

            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            // 🔹 Generate Access Token (15 minutes)
            var accessToken = GenerateJwtToken(claims, 15);

            // 🔹 Generate Refresh Token (Random String)
            var refreshToken = GenerateRefreshToken();

            // 🔹 Save Refresh Token in DB
            currentUser.RefreshToken = refreshToken;
            currentUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15)
            };
        }


        private string GenerateJwtToken(List<Claim> claims, int minutes)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(appSettingConfiguration["JWT:PrivateKey"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<AuthResponse> RefreshToken(string accessToken, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                return null;

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _unitOfWork._userRepository.FindById(userId);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null; // لازم يعمل Login من الأول
            }

            var newAccessToken = GenerateJwtToken(principal.Claims.ToList(), 15);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15)
            };
        }


        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(appSettingConfiguration["JWT:PrivateKey"])
                ),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
        public async Task Signout()
        {
            await _unitOfWork._userRepository.Signout();
        }

        public async Task DeleteAccount(string id)
        {
            var user = _unitOfWork._userRepository.FindById(id);
            if (user != null)
            {
                await _unitOfWork._userRepository.Delete(user.Result);
                await _unitOfWork.SaveChangesAsync();
            }
        }


    }
}
