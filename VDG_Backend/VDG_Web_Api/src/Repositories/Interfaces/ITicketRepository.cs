using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface ITicketRepository
	{
		public Task<TicketMessage?> GetTicketMessageAsync(int id);
		public Task<IEnumerable<TicketMessage>> GetTicketMessagesAsync(int ticketId);
		public Task<IEnumerable<Ticket>> GetTicketsAsync(int? doctorId = null, int? userId = null);
		public Task<Ticket?> GetTicketByIdAsync(int ticketId);
		public Task SendConsultationRequestAsync(Ticket ticket);
		public Task SendMessageAsync(TicketMessage ticketMessage);
		public Task UpdateMessageAsync(TicketMessage ticketMessage);
		public Task UpdateTicketStatusAsync(Ticket ticket);
		public Task DeleteMessageAsync(int Id);
	}
}
