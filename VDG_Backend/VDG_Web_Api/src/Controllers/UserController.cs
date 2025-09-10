using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VDG_Web_Api.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IClaimService _claimService;
        public UserController(IUserService userService, IClaimService claimService)
        {
            _userService = userService;
            _claimService = claimService;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = $"{nameof(UserRole.Admin)}")]
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

        [HttpGet("Profile")]
        public async Task<ActionResult<UserProfileDTO>> GetProfile()
        {
            try
            {
                var userProfile = await _userService.LoadUserProfile(_claimService.GetCurrentUserId());
                return userProfile;
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                if (!_claimService.GetCurrentUserRole().Equals(UserRole.Admin))
                {
                    id = _claimService.GetCurrentUserId();
                }

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
        public async Task<ActionResult<User?>> Put([FromBody] PersonProfileDTO user)
        {
            try
            {
                await _userService.UpdateUserAsync(user, _claimService.GetCurrentUserId());
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (!_claimService.GetCurrentUserRole().Equals(UserRole.Admin))
                {
                    id = _claimService.GetCurrentUserId();
                }

                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("ImageUrl")]
        public async Task<ActionResult> UpdateUserImage(int userId, string imageUrl)
        {
            try
            {
                await _userService.UpdateUserImageAsync(userId, imageUrl);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("ImageUrl")]
        public async Task<ActionResult<string>> GetUserImage(int userId)
        {
            try
            {
                var imageUrl = await _userService.GetUserImageAsync(userId);
                return Ok(imageUrl);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
