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
		private readonly IUserService _userService;


		public AuthController(IAuthService authService, IUserService userService)
		{
			_authService = authService;
			_userService = userService;
		}

		[HttpPost("Register")]
		public async Task<ActionResult> Register(UserRegister userRegister)
		{
			try
			{
				if (await _userService.AddUser(userRegister))
				{
					return Created();
				}

				return BadRequest("user has not been registered, Please try again");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpPost]
		public async Task<ActionResult> Authenticate(UserLogin user)
		{
			try
			{
				var response = await _authService.AuthenticateAsync(user);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
