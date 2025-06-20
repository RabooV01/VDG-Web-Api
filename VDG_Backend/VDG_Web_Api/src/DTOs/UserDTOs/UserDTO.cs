using System.ComponentModel.DataAnnotations;
using VDG_Web_Api.src.DTOs.PersonDTOs;

namespace VDG_Web_Api.src.DTOs.UserDTOs
{
    public class UserDTO
    {
        public int? Id { get; set; }

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Role { get; set; }

        public PersonDTO @Person { get; set; } = null!;
    }
}
