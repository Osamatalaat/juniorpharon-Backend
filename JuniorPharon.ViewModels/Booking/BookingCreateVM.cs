using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class BookingCreateVM : IValidatableObject
    {
        public int? TripId { get; set; }
        public int? PackageId { get; set; }

        [Required(ErrorMessage = "Client is required.")]
        public string ClientId { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Number of people is required.")]
        [Range(1, 1000, ErrorMessage = "Number of people must be at least 1")]
        public int NumberOfPeople { get; set; }

        // 🔥 Validation Logic (المهم)
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TripId == null && PackageId == null)
            {
                yield return new ValidationResult(
                    "You must select either Trip or Package.",
                    new[] { nameof(TripId), nameof(PackageId) }
                );
            }

            if (TripId != null && PackageId != null)
            {
                yield return new ValidationResult(
                    "You cannot select both Trip and Package.",
                    new[] { nameof(TripId), nameof(PackageId) }
                );
            }
        }
    }
}