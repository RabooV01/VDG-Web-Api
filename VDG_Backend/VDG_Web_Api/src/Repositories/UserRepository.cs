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

        }

        public User? GetById(int userId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            return user;
        }

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
}
