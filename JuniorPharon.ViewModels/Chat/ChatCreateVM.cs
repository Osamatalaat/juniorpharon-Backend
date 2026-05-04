

using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class ChatCreateVM
    {
        [Required]
        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }
    }
}
