using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;



namespace VDG_Web_Api.src.Services
{
	public class TicketService : ITicketService
	{
		private readonly int UpdateAndDeleteThreshold = 5;
		private readonly ITicketRepository _ticketRepository;


		public TicketService(ITicketRepository ticketRepository)
		{
			this._ticketRepository = ticketRepository;
		}

		// Done 
		public async Task DeleteMessageAsync(int id)
		{
			TicketMessage? ticketMessage = await _ticketRepository.GetTicketMessageAsync(id);

			if (ticketMessage == null)
			{
				throw new ArgumentNullException($"No such ticket with this id: {id}");
			}

			if (DateTime.Now.Subtract(ticketMessage.Date).Minutes > UpdateAndDeleteThreshold)
			{
				throw new Exception($"Cannot remove this message anymore");
			}

			try
			{
				await _ticketRepository.DeleteMessageAsync(id);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Faild to delete the ticket meassge, Error: {ex.Message}", ex);
			}
		}

		// Done 
		public async Task UpdateMessageAsync(TicketMessageDTO ticketMessageDTO)
		{

			TicketMessage ticketMessage = ticketMessageDTO.ToEntity();

			if (DateTime.Now.Subtract(ticketMessage.Date).Minutes > UpdateAndDeleteThreshold)
			{
				throw new InvalidOperationException($"The minimum time to remove the message has passed");
			}

			try
			{
				await _ticketRepository.UpdateMessageAsync(ticketMessage);
			}
			catch (Exception ex)
			{

				throw new InvalidOperationException($"Could not update", ex);
			}
		}
		// Done 
		public async Task<IEnumerable<DoctorTicketDTO>> GetDoctorConsultationsAsync(int doctorId)
		{
			try
			{
				var doctorConsultations = await _ticketRepository.GetConsultationsAsync(doctorId, null);

				return doctorConsultations.Select(d => d.ToDoctorTicketDto());
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"failed loading tickets", ex);
			}
		}
		// Done
		public async Task<IEnumerable<UserTicketDTO>> GetUserConsultationsAsync(int userId)
		{
			try
			{
				var userConsultaions = await _ticketRepository.GetConsultationsAsync(null, userId);

				return userConsultaions.Select(u => u.ToUserTicketDto());
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"failed loading tickets", ex);
			}
		}
		private bool HasTicketWithDoctor(IEnumerable<Ticket> tickets, int doctorId) =>
			tickets.Where(t => t.DoctorId == doctorId && t.Status != TicketStatus.Closed).Count() > 0;


		public async Task SendConsultationRequest(AddTicketDTO addTicketDTO)
		{
			if (string.IsNullOrEmpty(addTicketDTO.Text))
			{
				throw new ArgumentException("Ticket invalid");
			}

			var usetTickets = await _ticketRepository.GetConsultationsAsync(userId: addTicketDTO.UserId);

			if (HasTicketWithDoctor(usetTickets, addTicketDTO.DoctorId))
			{
				throw new Exception("You have an open ticket with this doctor");
			}

			try
			{
				Ticket ticket = addTicketDTO.ToEntity();
				ticket.TicketMessages = [new() { Text = addTicketDTO.Text, OwnerId = addTicketDTO.UserId, Date = DateTime.Now }];
				await _ticketRepository.SendConsultationRequestAsync(ticket);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Could not send, Error: {ex.Message}", ex);
			}
		}

		// Done 
		public async Task SendMessageAsync(TicketMessageDTO ticketMessageDto)
		{
			if (ticketMessageDto == null)
			{
				throw new ArgumentNullException("No such Message");
			}

			try
			{
				TicketMessage ticketMessage = ticketMessageDto.ToEntity();
				await _ticketRepository.SendMessageAsync(ticketMessage);
			}
			catch (Exception ex)
			{

				throw new InvalidOperationException($"Could not send, Error: {ex.Message}", ex);
			}
		}

		public async Task<IEnumerable<TicketMessageDTO>> GetTicketMessages(int ticketId)
		{
			try
			{
				var messages = await _ticketRepository.GetTicketMessagesAsync(ticketId);
				return messages.Select(m => m.ToDto());
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
