using JuniorPharon.Models.Enums;

namespace JuniorPharon.Models
{
    public class PackageContent
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public LanguageCode LanguageCode { get; set; }

        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
    }
}