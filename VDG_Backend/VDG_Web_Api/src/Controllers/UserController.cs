using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository userData;

		public UserController(IUserRepository userData)
		{
			this.userData = userData;
		}
		// GET: api/<UserController>
		[HttpGet]
		public IResult GetAllUsers(int page = 1, int limit = 20)
		{
			try
			{
				var users = userData.GetUsers(page, limit);
				return Results.Ok(users);
			}
			catch (Exception e)
			{
				return Results.BadRequest(e.Message);
			}
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public IResult Get(int id)
		{
			try
			{
				var user = userData.GetById(id);
				if (user == null)
				{
					return Results.NoContent();
				}
				return Results.Ok(user);
			}
			catch (Exception e)
			{
				return Results.BadRequest(e.Message);
			}

		}

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public IResult Delete(int id)
		{
			try
			{
				userData.DeleteUserAsync(id);
				return Results.NoContent();
			}
			catch (Exception e)
			{
				return Results.BadRequest(e.Message);
			}
		}
	}
}
