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

		public async Task<IEnumerable<TicketMessage>> GetTicketMessagesAsync(int tiketId)
		{
			try
			{
				return await _context.TicketMessages.Where(t => t.TicketId == tiketId).ToListAsync(); ;
			}
			catch (Exception ex)
			{

				throw new InvalidOperationException($"Error while retrieving data. {ex.Message}", ex);
			}
		}
		public async Task<IEnumerable<Ticket>> GetConsultationsAsync(int? doctorId = null, int? userId = null)
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
					return await tickets.Include(t => t.Doctor)
						.ThenInclude(t => t.User)
						.ThenInclude(u => u.Person)
						.Where(t => t.UserId == userId)
						.ToListAsync();
				}

				if (doctorId != null)
				{
					return await tickets
						.Include(t => t.User)
						.ThenInclude(u => u!.Person)
						.Where(t => t.DoctorId == doctorId)
						.ToListAsync();
				}

				throw new ArgumentException();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Error while retrieving data. {ex.Message}", ex);
			}
			throw new InvalidOperationException($"Unexpected error occured in {nameof(GetConsultationsAsync)} method controlflow.");
		}



		// Here we are 
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
		public async Task SendConsultationRequestAsync(Ticket ticket)
		{
			try
			{
				_context.Tickets.Add(ticket);
				await _context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Faild to add the ticket, Error: {e.Message}", e);
			}
		}

		public async Task SendMessageAsync(TicketMessage ticketMessage)
		{
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
		public async Task UpdateMessageAsync(TicketMessage ticketMessage)
		{
			var ticketMessageToUpdate = await _context.TicketMessages.FindAsync(ticketMessage.Id);
			if (ticketMessageToUpdate == null)
			{
				throw new KeyNotFoundException("The ticket message was not found.");
			}

			try
			{
				await _context.TicketMessages.Where(tm => tm.Id == ticketMessageToUpdate.Id)
					.ExecuteUpdateAsync(tm => tm.SetProperty(p => p.Text, ticketMessage.Text));
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Faild to update the ticket message, Error: {e.Message}", e);
			}

		}

		public async Task<TicketMessage?> GetTicketMessageAsync(int id)
		{
			try
			{
				return await _context.TicketMessages.FirstOrDefaultAsync(m => m.Id == id);
			}
			catch (Exception ex)
			{

				throw new Exception($"Faild while retriving data,Error:{ex.Message}", ex);
			}
		}
	}
}
