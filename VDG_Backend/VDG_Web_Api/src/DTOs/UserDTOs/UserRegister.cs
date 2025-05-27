using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using VDG_Web_Api.src.Models;
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f

namespace VDG_Web_Api.src.DTOs.UserDTOs
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
