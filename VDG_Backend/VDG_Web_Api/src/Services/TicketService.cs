using VDG_Web_Api.src.DTOs.TicketDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;



namespace VDG_Web_Api.src.Services
{
    public class TicketService : ITicketService
    {
        private readonly int UpdateAndDeleteThreshold = 5;
        private readonly ITicketRepository _ticketRepository;


        public TicketService(ITicketRepository ticketRepository)
        {
            this._ticketRepository = ticketRepository;
        }

        public async Task<TicketDTO> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);

                if (ticket == null)
                {
                    throw new KeyNotFoundException("No such ticket was found");
                }
                var date = ticket.TicketMessages.Select(t => t.Date).Order().FirstOrDefault();
                return ticket.ToDto(date);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Done 
        public async Task DeleteMessageAsync(int id)
        {
            TicketMessage? ticketMessage = await _ticketRepository.GetTicketMessageAsync(id);

            if (ticketMessage == null)
            {
                throw new ArgumentNullException(nameof(id), $"No such ticket with this id: {id}");
            }

            if (DateTime.Now.Subtract(ticketMessage.Date).Minutes > UpdateAndDeleteThreshold)
            {
                throw new Exception($"Cannot remove this message anymore");
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

            TicketMessage ticketMessage = ticketMessageDTO.ToEntity();

            if (DateTime.Now.Subtract(ticketMessage.Date).Minutes > UpdateAndDeleteThreshold)
            {
                throw new InvalidOperationException($"The minimum time to remove the message has passed");
            }

            try
            {
                await _ticketRepository.UpdateMessageAsync(ticketMessage);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not update", ex);
            }
        }
        // Done 
        public async Task<IEnumerable<DoctorTicketDTO>> GetDoctorConsultationsAsync(int doctorId)
        {
            try
            {
                var doctorConsultations = await _ticketRepository.GetTicketsAsync(doctorId, null);

                return doctorConsultations.Select(d => d.ToDoctorTicketDto(d.TicketMessages.OrderBy(t => t.Date).First()));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"failed loading tickets", ex);
            }
        }
        // Done

        public async Task<IEnumerable<UserTicketDTO>> GetUserConsultationsAsync(int userId)
        {
            try
            {
                var userConsultaions = await _ticketRepository.GetTicketsAsync(null, userId);

                return userConsultaions.Select(u => u.ToUserTicketDto(u.TicketMessages.OrderBy(t => t.Date).First()));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"failed loading tickets", ex);
            }
        }
        private bool HasTicketWithDoctor(IEnumerable<Ticket> tickets, int doctorId) =>
            tickets.Where(t => t.DoctorId == doctorId && t.Status != TicketStatus.Closed).Count() > 0;


        public async Task SendConsultationRequest(AddTicketDTO addTicketDTO)
        {
            if (string.IsNullOrEmpty(addTicketDTO.Text))
            {
                throw new ArgumentException("Ticket invalid");
            }

            var usetTickets = await _ticketRepository.GetTicketsAsync(userId: addTicketDTO.UserId);

            if (HasTicketWithDoctor(usetTickets, addTicketDTO.DoctorId))
            {
                throw new Exception("You have an open ticket with this doctor");
            }

            try
            {
                Ticket ticket = addTicketDTO.ToEntity();
                ticket.TicketMessages = [new() { Text = addTicketDTO.Text, OwnerId = addTicketDTO.UserId, Date = DateTime.Now }];
                await _ticketRepository.SendConsultationRequestAsync(ticket);
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
                TicketMessage ticketMessage = ticketMessageDto.ToEntity();
                await _ticketRepository.SendMessageAsync(ticketMessage);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Could not send, Error: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<TicketMessageDTO>> GetTicketMessages(int ticketId)
        {
            try
            {
                var messages = await _ticketRepository.GetTicketMessagesAsync(ticketId);
                return messages.Select(m => m.ToDto());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ChangeTicketStatus(int ticketId, TicketStatus ticketStatus)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);

            if (ticket == null)
            {
                throw new KeyNotFoundException("No such ticket was found");
            }

            bool isValidChange = (ticket.Status == TicketStatus.Open && ticketStatus == TicketStatus.Closed) ||
                (ticket.Status == TicketStatus.Pending && (ticketStatus == TicketStatus.Open || ticketStatus == TicketStatus.Rejected));

            if (!isValidChange)
            {
                throw new InvalidOperationException("cannot change state of this ticket");
            }

            ticket.Status = ticketStatus;

            if (ticketStatus == TicketStatus.Closed)
            {
                ticket.CloseDate = DateTime.Now;
            }

            try
            {
                await _ticketRepository.UpdateTicketStatusAsync(ticket);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteTicket(int ticketId)
        {
            try
            {
                await _ticketRepository.DeleteTicket(ticketId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
