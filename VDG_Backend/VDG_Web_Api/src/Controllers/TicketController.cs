using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Enums;
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
				//ticketDTO.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
				await _ticketService.SendConsultationRequest(ticketDTO);
				return Created();
			}
			catch (Exception)
			{
				return Problem();
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

		[HttpGet("{ticketId}/Messages")]
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

		[HttpPost("{ticketId}/Message")]
		public async Task<ActionResult> SendMessage(TicketMessageDTO ticketMessage)
		{
			try
			{
				//ticketMessage.OwnerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
				ticketMessage.Date = DateTime.Now;

				await _ticketService.SendMessageAsync(ticketMessage);

				return Created();
			}
			catch (Exception)
			{
				return Problem();
			}
		}

		[HttpPut("{ticketId}/Accept")]
		public async Task<ActionResult> AcceptTicket(int ticketId)
		{
			try
			{
				await _ticketService.ChangeTicketStatus(ticketId, TicketStatus.Open);
				return NoContent();
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpPut("{ticketId}/Reject")]
		public async Task<ActionResult> RejectTicket(int ticketId)
		{
			try
			{
				await _ticketService.ChangeTicketStatus(ticketId, TicketStatus.Rejected);
				return NoContent();
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpPut("{ticketId}/Close")]
		public async Task<ActionResult> CloseTicket(int ticketId)
		{
			try
			{
				await _ticketService.ChangeTicketStatus(ticketId, TicketStatus.Closed);
				return NoContent();
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
