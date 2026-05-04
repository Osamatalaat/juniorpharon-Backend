

using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class TripImageExt
    {
        public static TripImage ToCreate(this TripImageCreateVM Timage , string imgUrl)
        {
            return new TripImage
            {
                ImageUrl = imgUrl,
                IsCover = Timage.IsCover,

            };
        }

        public static TripImageDetailsVM ToDetails(this TripImage tripImage)
        {
            return new TripImageDetailsVM
            {
                Id = tripImage.Id,
                ImageUrl = tripImage.ImageUrl,
                IsCover = tripImage.IsCover,

            };
        }
    }
}
