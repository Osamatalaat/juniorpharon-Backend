



using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class TripCreateVM
    {
        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location must not exceed 100 characters.")]
        public string Location { get; set; }

        public string? CreatedByUserId { get; set; }// from claims


        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } 
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 100000, ErrorMessage = "Price must be greater than 0.")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days.")]
        public int DurationInDays { get; set; }

        //[Required(ErrorMessage = "Trip content in at least one language is required.")]
        //[MinLength(1, ErrorMessage = "You must provide at least one TripContent.")]
        public List<TripContentCreateVM> TripContents { get; set; }

        //[Required(ErrorMessage = "Trip path is required.")]
        //[MinLength(1, ErrorMessage = "Trip path must contain at least one location.")]
        //public List<string> TripPath { get; set; } = new();

        //public IFormFileCollection? TripImages { get; set; }  // Optional — use [Required] if needed
       // optional for test
        public List<TripImageCreateVM>? TripImages { get; set; }
    }

}
