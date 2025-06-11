using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		public async Task<ActionResult> RegisterUser(UserRegister user)
		{
			try
			{
				await _authService.AuthenticateAsync(user);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
