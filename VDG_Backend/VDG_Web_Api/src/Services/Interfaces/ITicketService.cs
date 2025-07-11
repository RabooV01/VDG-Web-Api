using VDG_Web_Api.src.DTOs.TicketDTOs;

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
	}
}
