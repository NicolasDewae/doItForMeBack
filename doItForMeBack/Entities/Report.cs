using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doItForMeBack.Entities
{
    public class Report
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [ForeignKey(name: "ClaimantId")]
        public User Claimant { get; set; }
        [ForeignKey(name: "UserId")]
        public User? User { get; set; }
        [ForeignKey(name: "MissionsId")]
        public Mission? Mission { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public string Subject { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        #endregion
    }
}
