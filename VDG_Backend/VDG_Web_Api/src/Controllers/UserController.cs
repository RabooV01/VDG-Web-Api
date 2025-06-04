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
        //public async Task<Task<ActionResult<IEnumerable<User>>>> GetAllUsers(int page = 1, int limit = 20)
        //{
        //    try
        //    {
        //        var users = await userData.GetUsers(page, limit);
        //        return users;
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var user = userData.GetById(id);
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
        public async Task<ActionResult<User?>> Put([FromBody] User user)
        {
            try
            {
                var updatedUser = await userData.UpdateUserAsync(user);
                return Ok(updatedUser);
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
