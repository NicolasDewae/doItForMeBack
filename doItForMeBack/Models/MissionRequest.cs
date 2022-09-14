using doItForMeBack.Entities;
using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Models
{
    public class MissionRequest
    {
        [Required]
        public int Id { get; set; }
        public string? Picture { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Tag { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime MissionDate { get; set; }
    }
}
