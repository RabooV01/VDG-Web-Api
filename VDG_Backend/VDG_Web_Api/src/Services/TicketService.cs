using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            this._ticketRepository = ticketRepository;
        }

        public async Task DeleteMessageAsync(int id)
        {
            try
            {
                await _ticketRepository.DeleteMessageAsync(id);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Faild to delete the ticket meassge, Error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<DoctorTicketDTO>> GetDoctorConsultationsAsync(string doctorId)
        {
            try
            {
                return (IEnumerable<DoctorTicketDTO>)await _ticketRepository.GetConsultationsAsync(doctorId, null);

            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<UserTicketDTO>> GetUserConsultationsAsync(int userId)
        {
            try
            {
                return (IEnumerable<UserTicketDTO>)await _ticketRepository.GetConsultationsAsync(null, userId);

            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
            }
        }

        public async Task SendConsultationRequest(TicketDTO ticketDto, TicketMessageDTO ticketMessageDto)
        {
            try
            {
                Ticket ticket = ticketDto;
                await _ticketRepository.SendConsultationRequestAsync(ticket);
                await SendMessageAsync(ticketMessageDto);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not send, Error: {ex.Message}", ex);
            }
        }

        public async Task SendMessageAsync(TicketMessageDTO message)
        {
            try
            {
                TicketMessage Message = message;
                await _ticketRepository.SendMessageAsync(Message);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not send, Error: {ex.Message}", ex);
            }
        }

        public async Task UpdateMessageAsync(TicketMessageDTO message)
        {
            try
            {
                await _ticketRepository.UpdateMessageAsync(message);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not update, Error: {ex.Message}", ex);
            }
        }
    }
}
