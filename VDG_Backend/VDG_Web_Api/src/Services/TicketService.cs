using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;


namespace VDG_Web_Api.src.Services
{
    public class TicketService : ITicketService
    {
        private readonly int UpdateAndDeleteThreshold = 5;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserService _userService;
        public readonly IDoctorService _doctorService;
        private readonly IUserRepository _userRepository;


        public TicketService(ITicketRepository ticketRepository, IUserService userService, IDoctorService doctorService, IUserRepository userRepository)
        {
            this._ticketRepository = ticketRepository;
            this._userService = userService;
            this._doctorService = doctorService;
            this._userRepository = userRepository;
        }
        // Done 
        public async Task DeleteMessageAsync(int id)
        {
            TicketMessage ticketMessage = await _ticketRepository.GetTicketMessageAsync(id);

            if (ticketMessage == null)
            {
                throw new ArgumentNullException($"No such ticket with this id: {id}");
            }

            if (DateTime.Now.Subtract(ticketMessage.Date).Minutes > UpdateAndDeleteThreshold)
            {
                throw new Exception($"The minimum time to remove the message has passed");
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

            var ticketMessage = MapToTicketMessage(ticketMessageDTO);
            if (ticketMessage == null)
            {
                throw new ArgumentNullException($"No such Message");
            }
            if (DateTime.Now.Subtract(ticketMessage.Date).Minutes > UpdateAndDeleteThreshold)
            {
                throw new Exception($"The minimum time to remove the message has passed");
            }

            try
            {
                await _ticketRepository.UpdateMessageAsync(ticketMessage);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not update, Error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<DoctorTicketDTO>> GetDoctorConsultationsAsync(string doctorId)
        {

            if (doctorId == null)
            {
                throw new ArgumentNullException($"No such doctor with this id: {doctorId}");
            }
            try
            {
                var doctorConsultations = await _ticketRepository.GetConsultationsAsync(doctorId, null);

                return doctorConsultations.Select(d =>
                {
                    var ticketDto = MapToTicketDto(d);

                    var user =
                    /* Todo 
                     
                        get Fname and Lname 

                    */

                    return new DoctorTicketDTO() { TicketDto = ticketDto, Fname = "", Lname = "" };
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

        public Task DeleteMessageAsync(int id, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
