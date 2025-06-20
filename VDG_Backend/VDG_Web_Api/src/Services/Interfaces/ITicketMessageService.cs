using VDG_Web_Api.src.DTOs.MessageDTOs;

namespace VDG_Web_Api.src.Services.Interfaces
{
    public interface ITicketMessageService
    {
        public Task<TicketMessageDTO> GetTicketMessage(int id);
    }
}
