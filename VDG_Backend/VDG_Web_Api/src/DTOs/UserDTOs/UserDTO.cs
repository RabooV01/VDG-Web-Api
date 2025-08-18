using System.ComponentModel.DataAnnotations;
using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.DTOs.UserDTOs
{
	public class UserDTO : PersonDTO
	{
		public int UserId { get; set; }

		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		public string Role { get; set; } = UserRole.User.ToString();
	}
}
