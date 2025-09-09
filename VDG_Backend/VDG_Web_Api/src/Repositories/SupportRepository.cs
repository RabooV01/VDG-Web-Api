using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
	public class SupportRepository : ISupportRepository
	{
		private readonly VdgDbDemoContext _context;

		public SupportRepository(VdgDbDemoContext context)
		{
			_context = context;
		}
		public async Task<int> GetTotalRows(Expression<Func<SupportModel, bool>>? expression)
		{
			if (expression == null)
			{
				return await _context.SupportModels.CountAsync();
			}

			return await _context.SupportModels.Where(expression).CountAsync();
		}

		public async Task AddMessage(SupportModel message)
		{
			try
			{
				if (string.IsNullOrEmpty(message.Message))
				{
					throw new ArgumentNullException(nameof(message.Message));
				}

				_context.Set<SupportModel>().Add(message);
				await _context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Error while retrieving data.", e);
			}
		}

		public async Task DeleteMessage(int messageId)
		{
			try
			{
				await _context.SupportModels.Where(s => s.Id == messageId)
					.ExecuteDeleteAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Unable to delete support message.", e);
			}
		}

		public async Task<IEnumerable<SupportModel>> GetSupportMessages(int page, int pageSize, string name)
		{
			try
			{
				var supportMessages = await _context.SupportModels.Include(s => s.User)
					.ThenInclude(s => s.Person)
					.Where(s => (s.User.Person.FirstName + " " + s.User.Person.LastName).Contains(name))
					.OrderBy(s => s.SentAt)
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync();

				return supportMessages;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task ReviewMessage(int messageId)
		{
			try
			{
				var message = await _context.SupportModels.FindAsync(messageId);
				if (message == null)
				{
					throw new ArgumentNullException(nameof(messageId), "Invalid Id");
				}

				message.Reviewed = true;

				_context.SupportModels.Update(message);
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
