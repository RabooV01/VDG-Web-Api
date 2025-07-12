using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TicketController : ControllerBase
	{
		private readonly ITicketService _ticketService;

		public TicketController(ITicketService ticketService)
		{
			_ticketService = ticketService;
		}
		[HttpPost]
		public async Task<ActionResult> OpenTicket(AddTicketDTO ticketDTO)
		{
			try
			{
				await _ticketService.SendConsultationRequest(ticketDTO);
				return Created();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("User/{userId}")]
		public async Task<ActionResult<IEnumerable<UserTicketDTO>>> GetUserTickets(int userId)
		{
			try
			{
				var tickets = await _ticketService.GetUserConsultationsAsync(userId);
				return Ok(tickets);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpGet("Doctor/{doctorId}")]
		public async Task<ActionResult<IEnumerable<DoctorTicketDTO>>> GetDoctorTickets(int doctorId)
		{
			try
			{
				var tickets = await _ticketService.GetDoctorConsultationsAsync(doctorId);
				return Ok(tickets);
			}
			catch (Exception)
			{

				return BadRequest();
			}
		}

		[HttpGet("{ticketId}")]
		public async Task<ActionResult<TicketDTO>> GetTicket(int ticketId)
		{
			try
			{
				var ticket = await _ticketService.GetTicketMessages(ticketId);
				return Ok(ticket);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
	}
}
