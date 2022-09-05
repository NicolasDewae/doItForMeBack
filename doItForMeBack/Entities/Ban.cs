using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doItForMeBack.Entities
{
    public class Ban
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [ForeignKey("BannerId")]
        public User? Banner { get; set; }

        [ForeignKey("WhoIsBannedId")]
        public User? WhoIsBanned { get; set; }
        
        public DateTime? BanDate { get; set; }
        public bool IsBan { get; set; }
        public string? Description { get; set; }
        #endregion
    }
}
