using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.UserDTOs
{
	public class UserDTO
	{
        public int? Id { get; set; }

        public string Email { get; set; } = null!;

        public string? Role { get; set; }
        
        public PersonDTO @Person { get; set; } = null!;
	}
}
