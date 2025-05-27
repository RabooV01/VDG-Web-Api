using System.ComponentModel.DataAnnotations;

namespace VDG_Web_Api.src.Data.DTOs.UserDTOs
{
    public class UserRegister
    {
        [Required]
        //Person? @Person { get; set; }

        //[Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; } = "user";
    }
}
