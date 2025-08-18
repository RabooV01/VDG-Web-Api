using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
	public class PromotionRequestRepository : IPromotionRequestRepository
	{
		private readonly VdgDbDemoContext _context;

		public PromotionRequestRepository(VdgDbDemoContext demoContext)
		{
			_context = demoContext;
		}

		public async Task Update(PromotionRequest request)
		{
			try
			{
				_context.PromotionRequests.Update(request);
				await _context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Could not approve request", e);
			}
		}

		public async Task Delete(int requestId)
		{
			try
			{
				await _context.PromotionRequests.Where(p => p.Id == requestId)
					.ExecuteDeleteAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Unable to delete request", e);
			}
		}

		public async Task<PromotionRequest?> GetById(int id)
		{
			try
			{
				var request = await _context.PromotionRequests.Include(p => p.User)
					.Include(p => p.Admin)
					.FirstOrDefaultAsync(p => p.Id == id);

				return request;
			}
			catch (Exception e)
			{
				throw new Exception("Error while retrieving data.", e);
			}
		}

		public async Task<IEnumerable<PromotionRequest>> GetPromotionRequests(int page, int pageSize, bool onlyPending = false, string? name = null)
		{
			try
			{
				Expression<Func<PromotionRequest, bool>> filter = p =>
				(name == null || $"{p.User.Person.FirstName} {p.User.Person.LastName ?? string.Empty}".Contains(name)) &&
				(!onlyPending || p.Status.Equals(PromotionStatus.Pending));

				var requests = await _context.PromotionRequests.AsNoTracking()
					.Include(p => p.User)
					.ThenInclude(u => u.Person)
					.Include(p => p.Admin)
					.ThenInclude(a => a.Person)
					.Where(filter)
					.OrderBy(p => p.RequestedAt)
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToListAsync();

				return requests;
			}
			catch (Exception e)
			{
				throw new Exception("Error while retrieving data", e);
			}
		}

		public async Task RequestPromotion(PromotionRequest request)
		{
			try
			{
				_context.PromotionRequests.Add(request);
				await _context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Could not add request", e);
			}
		}
	}
}
