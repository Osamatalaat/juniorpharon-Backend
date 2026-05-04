using Microsoft.AspNetCore.Http;


namespace JuniorPharon.ViewModels
{
    public class TripImageCreateVM
    {
        public IFormFile Image {  get; set; }

        public bool IsCover { get; set; } = false;
    }
}
