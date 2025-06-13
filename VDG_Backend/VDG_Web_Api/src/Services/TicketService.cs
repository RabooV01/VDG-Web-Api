using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;


namespace VDG_Web_Api.src.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserService _userService;


        public TicketService(ITicketRepository ticketRepository)
        {
            this._ticketRepository = ticketRepository;
        }
        // D 
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

        // N
        public async Task<IEnumerable<DoctorTicketDTO>> GetDoctorConsultationsAsync(string doctorId)
        {
            try
            {
                var doctorConsultations = await _ticketRepository.GetConsultationsAsync(doctorId, null);

                var userIds = doctorConsultations.Select(r => r.UserId)
                .Distinct()
                .ToList();

                UserDTO?[] userDtos = await Task.WhenAll(userIds.Where(Id => Id != null).Select(Id => _userService.GetUser(Id!.Value)));
                return doctorConsultations.Select(d =>
                {
                    var ticketDto = MapToTicketDto(d);

                    var userDto = userDtos.FirstOrDefault(user => user?.Id == ticketDto.UserId);

                    return new DoctorTicketDTO() { TicketDto = ticketDto, UserDto = userDto };
                });


            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
            }
        }

        // N
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

        // N
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

        // N 
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


        // N
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


        // Mapping 

        public TicketDTO MapToTicketDto(Ticket ticket)
        {
            return new TicketDTO()
            {
                CloseDate = ticket.CloseDate,
                Id = ticket.Id,
                DoctorId = ticket.DoctorId,
                Status = ticket.Status,
                UserId = ticket.UserId,

            };
        }
    }
}
