using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Entities
{
    public class Report
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public int ClaimantId { get; set; }
        public int? UserId { get; set; }
        public int? MissionId { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public string Subject { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        #endregion
    }
}
