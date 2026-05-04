using JuniorPharon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.Models
{
    public class TripContent
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public LanguageCode LanguageCode { get; set; }  

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }

}
