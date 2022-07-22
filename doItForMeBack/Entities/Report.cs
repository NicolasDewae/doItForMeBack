using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Entities
{
    public class Report
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public User? User { get; set; }
        public Mission? Mission { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public string Subject { get; set; }
        public DateTime CreationDate { get; set; }
        #endregion
    }
}
