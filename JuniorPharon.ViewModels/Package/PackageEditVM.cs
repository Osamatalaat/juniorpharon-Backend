using JuniorPharon.Models;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PackageEditVM : IValidatableObject
    {
        [MaxLength(200)]
        public string? Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public int? DurationInDays { get; set; }

        public int? MaxPeople { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        // 🔥 ممكن تعدل trips
        public List<PackageTripVM>? Trips { get; set; }

        // 🔥 وممكن تعدل pricing
        public List<PricingTierCreateVM>? PricingTiers { get; set; }

        // 🔥 Validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                if (EndDate <= StartDate)
                {
                    yield return new ValidationResult(
                        "End date must be greater than start date",
                        new[] { nameof(EndDate) }
                    );
                }
            }
        }
    }
}