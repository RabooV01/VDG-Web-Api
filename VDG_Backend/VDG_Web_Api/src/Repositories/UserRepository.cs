using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
	public class UserRepository : IUserRepository
	{
		VdgDbDemoContext context;


		public UserRepository(DbContextOptions<VdgDbDemoContext> context)
		{
			this.context = new VdgDbDemoContext(context);
		}

        public void DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public User? GetById(int userId)
		{
			var user = context.Users.FirstOrDefault(x => x.Id == userId);
			return user;
		}

		public IEnumerable<User> GetUsers()
		{

			User user = context.Users.FirstOrDefault();
			return new List<User>() { user };
		}

        public void UpdateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
