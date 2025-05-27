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

<<<<<<< HEAD
        public void DeleteUserAsync(int userId)
        {

        }
=======
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
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f

        public User? GetById(int userId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            return user;
        }

<<<<<<< HEAD
        public IEnumerable<User> GetUsers(int page, int pageSize)
        {
            var res = context.Users;

            return res;
        }

        public void UpdateUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
=======
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
>>>>>>> a2fa3bdb4ff82acadffb7131a2b2f90ba32e364f
}
