using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly VdgDbDemoContext context;

        public TicketRepository(VdgDbDemoContext context)
        {
            this.context = context;
        }
        public async Task DeleteMessage(int Id)
        {
            var ticketMessage = context.TicketMessages.FirstOrDefault(x => x.Id == Id);
            try
            {
                if (ticketMessage == null)
                {
                    throw new Exception("Message was not found");
                }
                context.TicketMessages.Remove(ticketMessage);
                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
        }

        public Task EditMessage(TicketMessage ticketMessage)
        {
            context.TicketMessages.Update(ticketMessage);
            context.SaveChangesAsync();
            return Task.CompletedTask;

        }

        public Task SendConsultationRequest(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task SendMessage(TicketMessage ticketMessage)
        {

            context.TicketMessages.Add(ticketMessage);
            context.SaveChangesAsync();
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Ticket>> ShowDoctorConsultationsAsync(int? doctorId = null, int? userId = null)
        {
            var doctor = context.Doctors.FirstOrDefault(x => x.UserId == Id);

            return (Task<IEnumerable<Ticket>>)doctor.Tickets;

        }
    }
}
