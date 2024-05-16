using System.ComponentModel.DataAnnotations;

namespace Service_Auth.Models
{
    public class RegistrationViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public ICollection<UserRole>? Roles { get; set; }
    }

}
