using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Services.Interfaces;
using VDG_Web_Api.src.Services.JWTService;
using VDG_Web_Api.src.Services.LocalizationService;

namespace VDG_Web_Api.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ILocalizationService _localizationService;


        public AuthController(IAuthService authService, IUserService userService, ILocalizationService localizationService)
        {
            _authService = authService;
            _userService = userService;
            _localizationService = localizationService;
        }

        [HttpGet("loc")]
        public async Task<ActionResult<LocationDto>> getloc(string name)
        {
            try
            {
                return Ok(await _localizationService.GetLocationAsync(name));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("HavDistanc")]
        public async Task<double> GetHaversineDistance(double x1, double y1, double x2, double y2)
        {
            return await _localizationService.HaversineDistance(x1, y1, x2, y2);

        }
        [HttpGet("EucDistance")]
        public async Task<double> GetEuclideanDistance(double x1, double y1, double x2, double y2)
        {
            return await _localizationService.EuclideanDistance(x1, y1, x2, y2);
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
        public async Task<ActionResult<AuthResponse>> Authenticate(UserLogin user)
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
