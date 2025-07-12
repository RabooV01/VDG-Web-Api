using System.ComponentModel.DataAnnotations;
using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.DTOs.UserDTOs
{
	public class UserDTO
	{
		public int Id { get; set; }

		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		public UserRole Role { get; set; } = UserRole.User;
		public PersonDTO @Person { get; set; } = null!;
	}
}
