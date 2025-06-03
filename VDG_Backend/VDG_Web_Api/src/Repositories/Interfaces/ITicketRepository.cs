using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        public Task<IEnumerable<Ticket>> ShowDoctorConsultationsAsync(int? doctorId, int? userId);
        public Task SendConsultationRequest(Ticket ticket);
        public Task SendMessage(TicketMessage ticketMessage);
        public Task EditMessage(TicketMessage ticketMessage);
        public Task DeleteMessage(int Id);
    }
}
