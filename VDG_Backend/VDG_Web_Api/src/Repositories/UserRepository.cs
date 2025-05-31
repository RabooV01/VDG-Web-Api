using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly VdgDbDemoContext context;


		public UserRepository(VdgDbDemoContext context)
		{
			this.context = context;
		}

		public Task DeleteUserAsync(int userId)
		{

			context.Users.Where(usr => usr.Id == userId).ExecuteDeleteAsync();
			context.SaveChanges();
			return Task.CompletedTask;
		}

		public async Task<User?> GetById(int userId)
		{
			var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
			return user;
		}

		public async Task<IEnumerable<User>> GetUsers(int page, int limit)
		{

			return await context.Users.ToListAsync();
		}

		public async Task<User?> UpdateUserAsync(User user)
		{
			context.Users.Update(user);
			await context.SaveChangesAsync();
			return user;
		}
	}
}
