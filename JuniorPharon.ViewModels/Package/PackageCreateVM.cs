using JuniorPharon.Models;
using JuniorPharon.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PackageCreateVM : IValidatableObject
    {
        // ❌ شيلنا Name و Description من هنا
        // لأنها بقت في PackageContent

        [Required]
        [Range(1, 365)]
        public int DurationInDays { get; set; }

        [Required]
        [Range(1, 1000)]
        public int MaxPeople { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // 🔥 Multi-language
        [Required]
        public List<PackageContentCreateVM> PackageContents { get; set; } = new();

        // 🔥 الرحلات جوه الباكدج
        [Required]
        public List<PackageTripCreateVM> Trips { get; set; } = new();

        // 🔥 التسعير
        [Required]
        public List<PricingTierCreateVM> PricingTiers { get; set; } = new();

        // 🔥 Validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "End date must be greater than start date",
                    new[] { nameof(EndDate) }
                );
            }

            if (!PackageContents.Any())
            {
                yield return new ValidationResult(
                    "Package must contain at least one language content",
                    new[] { nameof(PackageContents) }
                );
            }

            // 🔥 منع تكرار نفس اللغة
            if (PackageContents
                .GroupBy(c => c.LanguageCode)
                .Any(g => g.Count() > 1))
            {
                yield return new ValidationResult(
                    "Duplicate language is not allowed",
                    new[] { nameof(PackageContents) }
                );
            }

            if (!Trips.Any())
            {
                yield return new ValidationResult(
                    "Package must contain at least one trip",
                    new[] { nameof(Trips) }
                );
            }

            if (!PricingTiers.Any())
            {
                yield return new ValidationResult(
                    "Package must have pricing tiers",
                    new[] { nameof(PricingTiers) }
                );
            }
        }
    }
}