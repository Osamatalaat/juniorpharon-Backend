using JuniorPharon.Models.Enums;

namespace JuniorPharon.ViewModels
{
    public class PackageContentDetailsVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public LanguageCode LanguageCode { get; set; }
    }
}