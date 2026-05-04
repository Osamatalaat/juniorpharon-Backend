

using JuniorPharon.Models;
using JuniorPharon.Models.Enums;

namespace JuniorPharon.ViewModels
{
    public static class TripContentExt
    {

        public static TripContent ToCreate(this TripContentCreateVM content)
        {
            return new TripContent
            { 
                Title = content.Title,
                Description = content.Description,
                LanguageCode = content.LanguageCode,
            };
        }


        public static TripContentDetailsVM ToDetails(this TripContent content)
        {
            return new TripContentDetailsVM
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                LanguageCode = content.LanguageCode 
            };
        }

      
        public static TripContent ToEdit(this TripContentEditVM NewContent , TripContent OldContent)
        {
          
            OldContent.Title = string.IsNullOrEmpty(NewContent.Title) ? OldContent.Title : NewContent.Title;
            OldContent.Description = string.IsNullOrEmpty(NewContent.Description) ? OldContent.Description : NewContent.Description;
            

            return OldContent;
        }
    }
}
