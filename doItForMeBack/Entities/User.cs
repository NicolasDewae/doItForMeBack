using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doItForMeBack.Entities
{
    public class User
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Picture { get; set; }

        public List<Rate>? Rate { get; set; }

        [ForeignKey("Ban")]
        public int BanId { get; set; }
        public Ban? Ban { get; set; }

        public List<Mission>? Mission { get; set; }
        #endregion
    }
}
