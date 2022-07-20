using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Entities
{
    public class Mission
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public User Claimant { get; set; }
        public User Maker { get; set; } 
        public string Picture { get; set; }
        public List<Report> Report { get; set; }
        public Ban Ban { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Tag { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime MissionDate { get; set; }
        #endregion
    }
}
