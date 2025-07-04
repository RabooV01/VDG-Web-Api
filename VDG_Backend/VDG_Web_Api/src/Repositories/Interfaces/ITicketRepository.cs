using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        public Task<TicketMessage?> GetTicketMessageAsync(int id);
        public Task<IEnumerable<TicketMessage>> GetTicketMessagesAsync(int ticketId);
        public Task<IEnumerable<Ticket>> GetConsultationsAsync(int? doctorId = null, int? userId = null);
        public Task SendConsultationRequestAsync(Ticket ticket, TicketMessage ticketMessage);
        public Task SendMessageAsync(TicketMessage ticketMessage);
        public Task UpdateMessageAsync(TicketMessage ticketMessage);
        public Task DeleteMessageAsync(int Id);
    }
}
