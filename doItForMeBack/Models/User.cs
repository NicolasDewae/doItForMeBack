using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Models
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
        public string adress { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Rate { get; set; }
        public string? Picture { get; set; }
        #endregion
    }
}
