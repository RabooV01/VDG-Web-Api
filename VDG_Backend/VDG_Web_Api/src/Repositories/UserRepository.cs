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

		public void DeleteUserAsync(int userId)
		{
			var user = GetById(userId);
			if (user is null)
			{
				return;
			}
			context.Users.Remove(user);
			context.SaveChanges();
		}

		public User? GetById(int userId)
		{
			var user = context.Users.FirstOrDefault(x => x.Id == userId);
			return user;
		}

		public IEnumerable<User> GetUsers(int page, int limit)
		{

			return context.Users.AsEnumerable();
		}

		public void UpdateUserAsync(User user)
		{
			if (GetById(user.Id) == null)
			{
				return;
			}
			context.Users.Update(user);
		}
	}
}
