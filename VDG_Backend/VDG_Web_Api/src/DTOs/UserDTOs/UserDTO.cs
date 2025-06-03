using System.ComponentModel.DataAnnotations;
using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.UserDTOs
{
	public class UserDTO
	{
        public int Id { get; set; }

        public int PersonId { get; set; }

		public PersonDTO? Person { get; set; }
		
        public string Email { get; set; } = string.Empty;

        public string? Role { get; set; }
	}
}
