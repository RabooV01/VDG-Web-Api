using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.RatingDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetDoctorRate(int DoctorId)
        {
            try
            {
                var ratings = await _ratingService.Get(DoctorId);
                return ratings.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't get rating, Error {ex.Message}", ex);
            }
        }

        [HttpPost]

        public async Task<ActionResult> Rate(AddRatingDTO rate)
        {
            try
            {
                await _ratingService.Rate(rate);

                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error {ex.Message}", ex);
            }
        }

        [HttpPut]

        public async Task<ActionResult> Update(UpdateRatingDTO rate)
        {
            try
            {
                await _ratingService.Update(rate);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }

        [HttpDelete("Delete")]

        public async Task<ActionResult> Delete(int rateId)
        {
            try
            {
                await _ratingService.Delete(rateId);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }

    }
}
