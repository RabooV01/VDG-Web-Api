using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface IUserRepository
	{
		User? GetById(int userId);
		IEnumerable<User> GetUsers();
		void UpdateUserAsync(int userId);
		void DeleteUserAsync(int userId);
	}
}
