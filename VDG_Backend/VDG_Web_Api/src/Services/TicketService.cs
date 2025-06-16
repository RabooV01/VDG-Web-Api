using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserService _userService;
        public readonly IDoctorService _doctorService;


        public TicketService(ITicketRepository ticketRepository, IUserService userService, IDoctorService doctorService)
        {
            this._ticketRepository = ticketRepository;
            this._userService = userService;
            this._doctorService = doctorService;
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
                var doctorConsultations = await _ticketRepository.GetConsultationsAsync(doctorId, null);

                var userIds = doctorConsultations.Select(r => r.UserId)
                .Distinct()
                .ToList();

                var userDtos = await Task.WhenAll(userIds.Where(Id => Id != null).Select(Id => _userService.GetUser(Id!.Value)));
                return doctorConsultations.Select(d =>
                {
                    var ticketDto = MapToTicketDto(d);

                    var userDto = userDtos.FirstOrDefault(user => user?.Id == ticketDto.UserId);

                    return new DoctorTicketDTO() { TicketDto = ticketDto, UserDto = userDto };
                }).ToList();


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
                var userConsultaions = await _ticketRepository.GetConsultationsAsync(null, userId);

                var doctorIds = userConsultaions.Select(r => r.DoctorId)
                    .Distinct()
                    .ToList();
                var doctorDtos = await Task.WhenAll(doctorIds.Select(_doctorService.GetDoctorById!));

                return userConsultaions.Select(d =>
                {
                    var ticketDto = MapToTicketDto(d);
                    var doctorDto = doctorDtos.FirstOrDefault(doctor => doctor.SyndicateId == ticketDto.DoctorId);

                    return new UserTicketDTO()
                    {
                        TicketDto = ticketDto,
                        DoctorDto = doctorDto
                    };
                }).ToList();

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
                Ticket ticket = MapToTicket(ticketDto);
                await _ticketRepository.SendConsultationRequestAsync(ticket);
                await SendMessageAsync(ticketMessageDto);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Could not send, Error: {ex.Message}", ex);
            }
        }

        // N 
        public async Task SendMessageAsync(TicketMessageDTO ticketMessageDto)
        {
            try
            {
                TicketMessage ticketMessage = MapToTicketMessage(ticketMessageDto);
                await _ticketRepository.SendMessageAsync(ticketMessage);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not send, Error: {ex.Message}", ex);
            }
        }
        public async Task UpdateMessageAsync(TicketMessageDTO ticketMessageDTO)
        {
            try
            {
                var ticketMessage = MapToTicketMessage(ticketMessageDTO);
                await _ticketRepository.UpdateMessageAsync(ticketMessage);
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
                Id = ticket.Id,
                DoctorId = ticket.DoctorId,
                UserId = ticket.UserId,
                Status = ticket.Status,
                CloseDate = ticket.CloseDate,
            };
        }

        public Ticket MapToTicket(TicketDTO ticketDto)
        {
            return new Ticket()
            {
                DoctorId = ticketDto.DoctorId,
                Status = ticketDto.Status,
                UserId = ticketDto.UserId,
                CloseDate = ticketDto.CloseDate,
                Id = ticketDto.Id,
            };
        }

        public TicketMessage MapToTicketMessage(TicketMessageDTO ticketMessageDto)
        {
            return new TicketMessage()
            {
                Id = ticketMessageDto.Id,
                TicketId = ticketMessageDto.TicketId,
                Date = ticketMessageDto.Date,
                OwnerId = ticketMessageDto.OwnerId,
                Text = ticketMessageDto.Text,
            };
        }

    }
}
