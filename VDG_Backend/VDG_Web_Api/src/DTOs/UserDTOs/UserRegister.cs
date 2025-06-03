using System.ComponentModel.DataAnnotations;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.UserDTOs
{
	public class UserRegister
	{
		Person? @Person { get; set; }
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Role { get; } = "user";
	}
}
