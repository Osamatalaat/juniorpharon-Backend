

using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class UserExt
    {
        public static User ToCreate(this UserRegisterVM vm)
        {
            return new User
            {
                UserName = vm.UserName,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Gender = vm.Gender,
                CurrentCountry = vm.CurrentCountry,
                City = vm.City,
                Nationality = vm.Nationality,
                Age = vm.Age,
                ProfileImg = vm.ProfileImg,
                CreationDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
        }

        public static UserProfileVM ToDetails(this User user)
        {
            return new UserProfileVM
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Nationality = user.Nationality,
                Gender = user.Gender,
                Age = user.Age,
                ProfileImg = user.ProfileImg,
                City = user.City,
                CurrentCountry = user.CurrentCountry,
                CreationDate = user.CreationDate,
                NationalId = user.NationalId,
                //Role = user.Role
            };
        }

        public static User ToEdit(this UserUpdateProfileVM update, User user)
        {
            user.FirstName = string.IsNullOrWhiteSpace(update.FirstName) ? user.FirstName : update.FirstName;
            user.LastName = string.IsNullOrWhiteSpace(update.LastName) ? user.LastName : update.LastName;
            user.NationalId = string.IsNullOrWhiteSpace(update.NationalId) ? user.NationalId : update.NationalId;
            user.Nationality = string.IsNullOrWhiteSpace(update.Nationality) ? user.Nationality : update.Nationality;
            user.Age = update.Age.HasValue ? update.Age : user.Age;
            user.ProfileImg = string.IsNullOrWhiteSpace(update.ProfileImgUrl) ? user.ProfileImg : update.ProfileImgUrl;
            user.ModificationDate = DateTime.Now;
            user.Gender = update.Gender.HasValue ? update.Gender : user.Gender;

            return user;
        }

        //public static ResetPasswordVM ToResetPasswordVM(this User user)
        //{
        //    return new ResetPasswordVM
        //    {
        //        Email = user.Email
        //    };
        //}
    }
}
