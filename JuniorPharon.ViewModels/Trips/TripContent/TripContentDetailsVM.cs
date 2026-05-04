using JuniorPharon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.ViewModels
{
    public class TripContentDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LanguageCode LanguageCode { get; set; }  // Assuming LanguageCode is an enum

        
    }

}
