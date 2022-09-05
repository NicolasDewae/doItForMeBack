using System.ComponentModel.DataAnnotations;

namespace doItForMeBack.Models
{
    public class UserRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
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
    }
}
