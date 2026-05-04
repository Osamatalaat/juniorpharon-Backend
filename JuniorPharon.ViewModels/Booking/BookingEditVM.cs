using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class BookingEditVM
    {
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Number of people is required.")]
        [Range(1, 1000, ErrorMessage = "Number of people must be at least 1")]
        public int NumberOfPeople { get; set; }

        // 🔥 Validation إضافي
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate < DateTime.Now.Date)
            {
                yield return new ValidationResult(
                    "Start date cannot be in the past.",
                    new[] { nameof(StartDate) }
                );
            }
        }
    }
}