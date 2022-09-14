using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doItForMeBack.Entities
{
    public class Mission
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("ClaimantId")]
        public User Claimant { get; set; }
        [ForeignKey("StatusId")]
        public string Status { get; set; }
        //public enum Status { Nobody, Pending, Accepted }
        public List<User>? Maker { get; set; }
        public string? Picture { get; set; }
        public List<Report>? Report { get; set; }
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
        #endregion
    }
}
