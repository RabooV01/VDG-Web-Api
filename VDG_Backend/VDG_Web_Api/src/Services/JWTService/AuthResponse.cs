namespace VDG_Web_Api.src.Services.JWTService
{
	public class AuthResponse
	{
		public Token Token { get; set; }
		public UserAuthData User { get; set; }
	}
}
