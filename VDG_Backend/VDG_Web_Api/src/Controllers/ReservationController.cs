using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationController : ControllerBase
	{
        private readonly IReservationService _reservationService;

        //TODO Reservation Operations and routings
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

		[HttpGet("User/{userId}")]
		public async Task<ActionResult<IEnumerable<UserReservationDTO>>> GetUserReservations([FromRoute] int userId, [FromQuery] DateOnly? date)
		{
			try
			{
				var reservations = await _reservationService.GetUserReservationsAsync(userId, date);
				return Ok(reservations.ToList());
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
		[HttpGet("Clinic/{clinicId}")]
		public async Task<ActionResult<IEnumerable<ClinicReservationDTO>>> GetClinicReservations([FromRoute] int clinicId, [FromQuery] DateOnly? date)
		{
			try
			{
				var reservations = await _reservationService.GetClinicReservationsAsync(clinicId, date);
				return Ok(reservations.ToList());
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
    }
}
