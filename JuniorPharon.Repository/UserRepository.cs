

using Infrastructure.SqlServer;
using JuniorPharon.Models;
using JuniorPharon.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace JuniorPharon.Repository
{
    public class UserRepository : BaseRepository<User>
    {

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public UserRepository(DBContext context , UserManager<User> userManager , SignInManager<User> signInManager) : base(context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public async Task<IdentityResult> Register(UserRegisterVM accountRegister)
        {
            try
            {
                var user = accountRegister.ToCreate();
                //user.UserName = accountRegister.Email;

                //Must Send UserName, Identity Requires Username even if not used
                var res = await userManager.CreateAsync(user, accountRegister.Password);

                if (res.Succeeded)
                {
                    User account = await userManager.FindByEmailAsync(accountRegister.Email);

                    res = await userManager.AddToRoleAsync(account, accountRegister.Role);

                }
                return res;
            }
            catch
            {
                throw;
            }

        }


        public async Task<SignInResult> Login(UserLoginVM accountLogin)
        {
            var User = await userManager.FindByEmailAsync(accountLogin.UserNameOrEmail);

            if (User != null)
            {
                return await signInManager.PasswordSignInAsync(User, accountLogin.Password, true, true);
            }
            else
            {
                return await signInManager.PasswordSignInAsync(accountLogin.UserNameOrEmail, accountLogin.Password, true, true);
            }
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            try
            {
                return await userManager.FindByEmailAsync(email);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IList<string>> GetUserRoles(User user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<User> FindById(string Id)
        {
            try
            {

                return await userManager.FindByIdAsync(Id);
            }
            catch
            {
                throw;
            }
        }

        public async Task Signout()
        {
            try
            {
                await signInManager.SignOutAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
