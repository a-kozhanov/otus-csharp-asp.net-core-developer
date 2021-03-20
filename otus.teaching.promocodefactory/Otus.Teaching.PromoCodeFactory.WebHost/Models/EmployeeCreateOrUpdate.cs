using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class EmployeeCreateOrUpdate
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [MinLength(1), MaxLength(32)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1), MaxLength(32)]
        public string LastName { get; set; }
    }
}