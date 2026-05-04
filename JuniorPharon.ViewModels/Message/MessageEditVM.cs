using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.ViewModels
{
    public class MessageEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Message content is required.")]
        //[StringLength(1000, ErrorMessage = "Message too long.")]
        public string Content { get; set; }
    }
}
