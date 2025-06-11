using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}
		// GET: api/<UserController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers(int page = 1, int limit = 20)
		{
			try
			{
				var users = await _userService.GetUsers(page, limit);
				return Ok(users);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int id)
		{
			try
			{
				var user = await _userService.GetUser(id);
				if (user == null)
				{
					return NoContent();
				}
				return Ok(user);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		// PUT api/<UserController>/5
		[HttpPut]
		public async Task<ActionResult<User?>> Put([FromBody] UserDTO user)
		{
			try
			{
				await _userService.UpdateUserAsync(user);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public IResult Delete(int id)
		{
			try
			{
				_userService.DeleteUserAsync(id);
				return Results.NoContent();
			}
			catch (Exception e)
			{
				return Results.BadRequest(e.Message);
			}
		}
	}
}
