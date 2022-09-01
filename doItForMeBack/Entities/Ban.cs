using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doItForMeBack.Entities
{
    public class Ban
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [ForeignKey("Banner")]
        public int? BannerId { get; set; }
        public User? Banner { get; set; }

        [ForeignKey("WhoIsBanned")]
        public int? WhoIsBannedId { get; set; }
        public User? WhoIsBanned { get; set; }
        
        public DateTime? BanDate { get; set; }
        public bool IsBan { get; set; }
        public string? Description { get; set; }
        #endregion
    }
}
