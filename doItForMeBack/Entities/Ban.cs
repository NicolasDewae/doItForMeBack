using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Entities
{
    public class Ban
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public User UserBan { get; set; }
        [Required]
        public DateTime BanDate { get; set; }
        public bool IsBan { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
