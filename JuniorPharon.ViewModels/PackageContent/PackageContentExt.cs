using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class PackageContentExt
    {
        // 🔹 Create
        public static PackageContent ToCreate(this PackageContentCreateVM vm)
        {
            return new PackageContent
            {
                Name = vm.Name,
                Description = vm.Description,
                LanguageCode = vm.LanguageCode
            };
        }

        // 🔹 Details
        public static PackageContentDetailsVM ToDetails(this PackageContent model)
        {
            return new PackageContentDetailsVM
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                LanguageCode = model.LanguageCode
            };
        }

        // 🔹 Edit
        public static PackageContent ToEdit(this PackageContentEditVM vm, PackageContent model)
        {
            if (!string.IsNullOrEmpty(vm.Name))
                model.Name = vm.Name;

            if (!string.IsNullOrEmpty(vm.Description))
                model.Description = vm.Description;

            if (vm.LanguageCode.HasValue)
                model.LanguageCode = vm.LanguageCode.Value;

            return model;
        }
    }
}