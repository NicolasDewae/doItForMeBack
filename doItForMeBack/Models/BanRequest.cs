using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Models
{
    public class BanRequest
    {
        [Required]
        public int WhoIsBannedId { get; set; }
        [Required]
        public bool IsBan { get; set; }
        public string? Description { get; set; }
    }
}
