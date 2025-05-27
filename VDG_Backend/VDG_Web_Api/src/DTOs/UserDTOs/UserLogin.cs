using System.ComponentModel.DataAnnotations;

namespace VDG_Web_Api.src.DTOs.UserDTOs
{
	public class UserLogin
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
