using System.ComponentModel.DataAnnotations;

namespace VDG_Web_Api.src.Services.JWTService
{
	public class UserAuthData
	{
		public int UserId { get; set; }

		[EmailAddress]
		public string Email { get; set; }
		public string Role { get; set; }
		public string FirstName { get; set; }
		public string? LastName { get; set; }
		public int? DoctorId { get; set; } = null;
		public string? ImageUrl { get; set; }
	}
}
