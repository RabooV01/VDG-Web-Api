using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using VDG_Web_Api.src.Models;
=======
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f
using VDG_Web_Api.src.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VDG_Web_Api.src.Controllers
{
<<<<<<< HEAD
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> GetAllUsers(IUserRepository repo)
        {
            var users = repo.GetUsers(1, 1);
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
=======
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
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

<<<<<<< HEAD
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        //[HttpGet("/login")]
        //public UserLogin Get(UserRepository userdata)
        //{
        //    return userdata.GetUserLogin(new Data.Models.User() { Email = "rabii@rabii.com", PasswordHash = "asdasdasd" });
        //}
    }
=======
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
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f
}
