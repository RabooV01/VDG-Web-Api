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
		// GET: api/<UserController>
		[HttpGet]
		public IEnumerable<User> GetAllUsers(IUserRepository repo)
		{
			var users = repo.GetUsers();
			return users;
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<UserController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
