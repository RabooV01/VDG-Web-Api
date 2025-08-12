using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.Services.Interfaces
{
	public interface ITicketService
	{
		public Task<IEnumerable<DoctorTicketDTO>> GetDoctorConsultationsAsync(int doctorId);
		public Task<IEnumerable<UserTicketDTO>> GetUserConsultationsAsync(int userId);
		public Task SendMessageAsync(TicketMessageDTO message);
		public Task UpdateMessageAsync(TicketMessageDTO message);
		public Task DeleteMessageAsync(int id);
		public Task SendConsultationRequest(AddTicketDTO addTicketDTO);
		public Task<IEnumerable<TicketMessageDTO>> GetTicketMessages(int ticketId);
		public Task ChangeTicketStatus(int ticketId, TicketStatus ticketStatus);
		public Task<TicketDTO> GetTicketByIdAsync(int ticketId);
		public Task DeleteTicket(int ticketId);
	}
}
