using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<User?> GetById(int userId);
		Task<IEnumerable<User>> GetUsers(int page, int limit);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(int userId);
		Task<int> AddUserAsync(User user);
		Task<User?> GetByEmail(string email);
	}
}
