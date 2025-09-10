using System.ComponentModel.DataAnnotations;
using VDG_Web_Api.src.DTOs.PersonDTOs;

namespace VDG_Web_Api.src.DTOs.UserDTOs
{
	public class UserRegister
	{
		public string? ImageUrl { get; set; }
		[EmailAddress]
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public PersonProfileDTO @Person { get; set; } = null!;
	}
}
