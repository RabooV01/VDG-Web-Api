using VDG_Web_Api.src.DTOs.DoctorDTOs;
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


        public TicketService(ITicketRepository ticketRepository, IUserService userService, IDoctorService doctorService)
        {
            this._ticketRepository = ticketRepository;
            this._userService = userService;
            this._doctorService = doctorService;
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
            if (ticketMessageDTO == null)
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
        // Done 
        public async Task<IEnumerable<DoctorTicketDTO>> GetDoctorConsultationsAsync(int doctorId)
        {

            if (doctorId <= 0)
            {
                throw new Exception($"No such doctor with this id: {doctorId}");
            }
            try
            {
                var doctorConsultations = await _ticketRepository.GetConsultationsAsync(doctorId, null);

                return (IEnumerable<DoctorTicketDTO>)doctorConsultations.Select(async d =>
                {
                    var ticketDto = MapToTicketDto(d);

                    var user = await _userService.GetUser(ticketDto.UserId ?? 0);

                    var fname = user?.Person.FirstName;
                    var lname = user?.Person.LastName;

                    return new DoctorTicketDTO() { TicketDto = ticketDto, UserFirstName = fname, UserLastName = lname };
                }).ToList();


            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
            }
        }
        // Done
        public async Task<IEnumerable<UserTicketDTO>> GetUserConsultationsAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new Exception($"No such doctor with this id: {userId}");
            }
            try
            {
                var userConsultaions = await _ticketRepository.GetConsultationsAsync(null, userId);

                return userConsultaions.Select(u =>
                {
                    var ticketDto = MapToTicketDto(u);

                    var doctorDto = _doctorService.GetDoctorById(ticketDto.DoctorId ?? 0);

                    return new UserTicketDTO() { DoctorDto = doctorDto, TicketDto = ticketDto };

                });
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
            }
        }
        // Done
        public async Task SendConsultationRequest(TicketDTO ticketDto, TicketMessageDTO ticketMessageDto)
        {
            if (ticketDto == null || ticketMessageDto == null)
            {
                throw new ArgumentNullException($"No such Message");
            }
            if (_ticketRepository.)
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

        // Done 
        public async Task SendMessageAsync(TicketMessageDTO ticketMessageDto)
        {
            if (ticketMessageDto == null)
            {
                throw new ArgumentNullException("No such Message");
            }
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

        public DoctorDTO MapToDoctorDto(Doctor? doctor)
        {
            return new DoctorDTO()
            {
                Speciality = doctor.Speciality,
                Id = doctor.Id,
                SpecialityId = doctor.SpecialityId,
                SyndicateId = doctor.SyndicateId,
                UserId = doctor.UserId
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
