using System.ComponentModel.DataAnnotations;

namespace VendorAPI.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CompanyCode { get; set; }
        public string BotEmail { get; set; }
        public string Description { get; set; }
        public string PanNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
