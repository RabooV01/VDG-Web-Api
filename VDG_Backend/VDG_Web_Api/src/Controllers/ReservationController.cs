using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Models;
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
				return Ok(reservations);
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
		public async Task<ActionResult<IEnumerable<ClinicReservationDTO>>> GetClinicReservations([FromRoute] int clinicId, [FromQuery] DateTime date)
		{
			try
			{
				var reservations = await _reservationService.GetClinicReservationsAsync(clinicId, date);
				return Ok(reservations);
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

		[HttpPost("Revision")]
		public async Task<ActionResult> MakeRevision(ReservationDTO reservationDTO)
		{
			try
			{
				reservationDTO.Type = BookingTypes.Revision;
				await _reservationService.BookAppointmentAsync(reservationDTO);

				return Created();
			}
			catch (Exception)
			{

				throw;
			}
		}

		//authorized to both user and doctor
		[HttpPost("Preview")]
		public async Task<ActionResult> MakePreviewReservation(ReservationDTO reservationDTO)
		{
			try
			{
				reservationDTO.Type = BookingTypes.Preview;
				await _reservationService.BookAppointmentAsync(reservationDTO);

				//TODO must schedule confirmation payment

				return Created();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpPut]
		public async Task<ActionResult> UpdateAppointment(ReservationDTO r)
		{
			try
			{
				if (await _reservationService.EditAppointmentAsync(r))
				{
					return NoContent();
				}

				return BadRequest("cannot update appointment, make sure it is not confirmed and has no conflicts");
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return Problem();
			}
		}

		[HttpDelete("{reservationId}")]
		public async Task<ActionResult> DeleteReservation(int reservationId)
		{
			try
			{
				await _reservationService.CancelAppointmentAsync(reservationId);
				return NoContent();
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (KeyNotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return Problem($"Something went wrong. {ex.Message}");
			}
		}
	}
}
