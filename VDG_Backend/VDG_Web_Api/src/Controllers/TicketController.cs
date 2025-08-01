﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class TicketController : ControllerBase
	{
		private readonly ITicketService _ticketService;
		private readonly IClaimService _claimService;
		private readonly IDoctorService _doctorService;

		public TicketController(ITicketService ticketService, IClaimService claimService, IDoctorService doctorService)
		{
			_ticketService = ticketService;
			_claimService = claimService;
			_doctorService = doctorService;
		}
		[HttpPost]
		public async Task<ActionResult> OpenTicket(AddTicketDTO ticketDTO)
		{
			try
			{
				ticketDTO.UserId = _claimService.GetCurrentUserId();
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
				if (!_claimService.GetCurrentUserRole().Equals(UserRole.Admin))
				{
					userId = _claimService.GetCurrentUserId();
				}

				var tickets = await _ticketService.GetUserConsultationsAsync(userId);
				return Ok(tickets);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpGet("Doctor/{doctorId}")]
		[Authorize(Policy = "Doctor-Admin")]
		public async Task<ActionResult<IEnumerable<DoctorTicketDTO>>> GetDoctorTickets(int doctorId)
		{
			try
			{
				var doctor = await _doctorService.GetDoctorById(doctorId);
				int currentUserId = _claimService.GetCurrentUserId();
				if (doctor.UserId != currentUserId && !_claimService.GetCurrentUserRole().Equals(UserRole.Admin))
				{
					return Unauthorized();
				}
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
				var ticket = await _ticketService.GetTicketByIdAsync(ticketId);

				int currentUserId = _claimService.GetCurrentUserId();
				int? doctorId = null;

				if (_claimService.GetCurrentUserRole().Equals(UserRole.Doctor))
				{
					doctorId = (await _doctorService.GetDoctorById(ticket.DoctorId)).DoctorId;
				}

				bool isAdmin = _claimService.GetCurrentUserRole().Equals(UserRole.Admin);


				if (!isAdmin && ticket.UserId != currentUserId && currentUserId != doctorId)
				{
					return Unauthorized();
				}

				var ticketMessages = await _ticketService.GetTicketMessages(ticketId);
				return Ok(ticketMessages);
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
				var ticket = await _ticketService.GetTicketByIdAsync(ticketMessage.TicketId);
				ticketMessage.OwnerId = _claimService.GetCurrentUserId();
				ticketMessage.Date = DateTime.Now;

				await _ticketService.SendMessageAsync(ticketMessage);

				return Created();
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

		[HttpPut("{ticketId}/Accept")]
		[Authorize(Policy = "Doctor-Admin")]
		public async Task<ActionResult> AcceptTicket(int ticketId)
		{
			try
			{
				var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
				int currentUserId = _claimService.GetCurrentUserId();
				int? doctorId = null;

				if (_claimService.GetCurrentUserRole().Equals(UserRole.Doctor))
				{
					doctorId = (await _doctorService.GetDoctorById(ticket.DoctorId)).DoctorId;
				}

				bool isAdmin = _claimService.GetCurrentUserRole().Equals(UserRole.Admin);

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
		[Authorize(Policy = "Doctor-Admin")]
		public async Task<ActionResult> RejectTicket(int ticketId)
		{
			try
			{
				var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
				int currentUserId = _claimService.GetCurrentUserId();
				int? doctorId = null;

				if (_claimService.GetCurrentUserRole().Equals(UserRole.Doctor))
				{
					doctorId = (await _doctorService.GetDoctorById(ticket.DoctorId)).DoctorId;
				}

				bool isAdmin = _claimService.GetCurrentUserRole().Equals(UserRole.Admin);

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
		[Authorize(Policy = "Doctor-Admin")]
		public async Task<ActionResult> CloseTicket(int ticketId)
		{
			try
			{
				var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
				int currentUserId = _claimService.GetCurrentUserId();
				int? doctorId = null;

				if (_claimService.GetCurrentUserRole().Equals(UserRole.Doctor))
				{
					doctorId = (await _doctorService.GetDoctorById(ticket.DoctorId)).DoctorId;
				}

				bool isAdmin = _claimService.GetCurrentUserRole().Equals(UserRole.Admin);

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
