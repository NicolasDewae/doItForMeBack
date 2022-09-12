using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doItForMeBack.Entities
{
    public class Rate
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [ForeignKey(name: "UserRateId")]
        public User UserRate { get; set; }
        [ForeignKey(name: "UserRatedId")]
        public User UserRated { get; set; }
        public float Star { get; set; }
        #endregion
    }
}

