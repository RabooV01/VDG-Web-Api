using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.Services.JWTService
{
	public class AuthResponse
	{
		public Token Token { get; set; }
		public UserDTO User { get; set; }
	}
}
