using JuniorPharon.Services;
using JuniorPharon.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace JuniorPharon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class AccountController : ControllerBase
    {
        private readonly AccountService accountService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(AccountService accountService, RoleManager<IdentityRole> roleManager)
        {
            this.accountService = accountService;
            _roleManager = roleManager;
        }


        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return new JsonResult(new
            {
                Massage = "Role name is required",
                Status = 400
            });

            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (roleExists)
                return new JsonResult(new
                {
                    Massage = "Role already exists",
                    Status = 400
                });

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            
            return new JsonResult(new
            {
                Massage = "Role created successfully",
                Status = 200
            });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterVM user)
        {

            if (ModelState.IsValid)
            {
                var res = await accountService.CreateAccount(user);
                if (res.IsSuccess)
                {
                    return new JsonResult(res);
                }

                return new JsonResult(res);

            }

            StringBuilder stringBuilder1 = new StringBuilder();
            foreach (var item in ModelState.Values)
            {
                foreach (var err in item.Errors)
                {
                    stringBuilder1.Append(err.ErrorMessage);
                }
            }
            return new JsonResult(new
            {
                Massage = stringBuilder1.ToString(),
                Status = 400
            });
        }




        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginVM vmodel)
        {
            if (ModelState.IsValid)
            {
                var res = await accountService.LoginWithToken(vmodel);
                if (res == null)
                {
                    return new JsonResult(res);
                }
                else if (res == null)
                {
                    return new JsonResult(new
                    {
                        Massage = "Sorry try again Later!!!! Your Accout under Review",
                        Status = 400
                    });
                }
                else
                {

                    var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

                    var user = await accountService.GetUserById(userId);

                    return new JsonResult(new
                    {
                        Massage = "Logged in Successfully",
                        Status = 200,
                        Token = res,
                        Role = role,
                        Image = user.ProfileImg,
                        FullName = user.FirstName + " " + user.LastName,
                        Email = email,
                    });

                }
            }

            StringBuilder stringBuilder1 = new StringBuilder();
            foreach (var item in ModelState.Values)
            {
                foreach (var err in item.Errors)
                {
                    stringBuilder1.Append(err.ErrorMessage);
                }
            }

            return new JsonResult(new
            {
                Massage = stringBuilder1.ToString(),
                Status = 400
            });

        }



        [HttpPost("Signout")]
        public async Task<IActionResult> Signout()
        {
            await accountService.Signout();
            return new JsonResult(new
            {
                Massage = "Sign out Successfully",
                Status = 200
            });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var res = await accountService.RefreshToken(
                request.AccessToken,
                request.RefreshToken
            );

            if (res == null)
                return Unauthorized(new { Message = "Session expired. Please login again." });

            return Ok(new
            {
                Message = "Token Refreshed Successfully",
                Status = 200,
                Token = res
            });
        }



        [HttpDelete("DeleteAccount/{accId}")]
        public async Task<IActionResult> DeleteAccount(string accId)
        {
            await accountService.DeleteAccount(accId);
            return new JsonResult(new
            {
                Massage = "Account Deleted Successfully.",
                Status = 200
            });
        }

    }
}
