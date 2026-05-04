
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.ViewModels
{
    public class TripEditVM
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public float Price { get; set; }
        public int DurationInDays { get; set; }
        public List<TripContentEditVM> Contents { get; set; } = new();

        public List<string>? TripPath { get; set; } = new();
        public IFormFileCollection? TripImages { get; set; } // Optional — use [Required] if needed
    }
}
