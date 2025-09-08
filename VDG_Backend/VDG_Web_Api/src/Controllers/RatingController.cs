using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.RatingDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("DoctorRate/{doctoId}")]

        public async Task<ActionResult<RatingDTO>> GetDoctorRate([FromRoute] int DoctorId)
        {
            try
            {
                var rate = await _ratingService.Get(DoctorId);

                return rate;
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't get rating, Error{ex.Message}", ex);
            }
        }

        [HttpPost("Rate")]

        public async Task<ActionResult> Rate(RatingDTO rate)
        {
            try
            {
                await _ratingService.Rate(rate);

                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }

        [HttpPut("Update")]

        public async Task<ActionResult> Update(RatingDTO rate)
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

        public async Task<ActionResult> Delete(RatingDTO rate)
        {
            try
            {
                await _ratingService.Delete(rate);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error{ex.Message}", ex);
            }
        }

    }
}
