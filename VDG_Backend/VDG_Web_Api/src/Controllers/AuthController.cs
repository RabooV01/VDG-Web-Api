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
				if (await _authService.AuthenticateAsync(user))
				{
					return Created();
				}
				return BadRequest("Check that your email address is valid, and please write password longer than 8 characters");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
