using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly VdgDbDemoContext _context;

        public TicketRepository(VdgDbDemoContext context)
        {
            this._context = context;
        }
        // Done
        public async Task<IEnumerable<Ticket>> GetConsultationsAsync(string? doctorId = null, int? userId = null)
        {
            if (doctorId != null && userId != null)
            {
                throw new ArgumentNullException("Should select a doctor or user");
            }
            try
            {
                var tickets = _context.Tickets.AsQueryable();

                if (userId != null)
                {
                    return await tickets.Where(t => t.UserId == userId)
                        .ToListAsync();
                }
                if (doctorId != null)
                {
                    return await tickets.Where(t => t.DoctorId == doctorId)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while retrieving data. {ex.Message}", ex);
            }
            throw new InvalidOperationException($"Unexpected error occured in {nameof(GetConsultationsAsync)} method controlflow.");
        }

        // Done
        public async Task DeleteMessageAsync(int id)
        {
            var message = await _context.TicketMessages.FindAsync(id);

            if (message == null)
            {
                throw new KeyNotFoundException("There is no message with this id");
            }
            try
            {
                _context.TicketMessages.Remove(message);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new InvalidOperationException($"Faild to delet the message, Error: {e.Message}", e);
            }
        }

        // #ToDo Add service logic (create + sendMessage) on request
        public async Task SendConsultationRequestAsync(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new KeyNotFoundException("The ticket has not found.");
            }
            try
            {
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new InvalidOperationException($"Faild to send the ticket, Error: {e.Message}", e);
            }
        }

        // Done
        public async Task SendMessageAsync(TicketMessage ticketMessage)
        {
            if (ticketMessage == null)
            {
                throw new KeyNotFoundException("The ticket message has not found.");
            }
            try
            {
                _context.TicketMessages.Add(ticketMessage);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new InvalidOperationException($"Faild to send the ticket message, Error: {e.Message}", e);
            }
        }

        // Done
        public async Task UpdateMessageAsync(TicketMessage ticketMessage)
        {
            var ticketMessageToUpdate = await _context.TicketMessages.FindAsync(ticketMessage.Id);
            if (ticketMessageToUpdate == null)
            {
                throw new KeyNotFoundException("The ticket message has not found.");
            }

            ticketMessageToUpdate = ticketMessage;

            try
            {
                _context.TicketMessages.Update(ticketMessageToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new InvalidOperationException($"Faild to update the ticket message, Error: {e.Message}", e);
            }

        }
    }
}
