using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IUserService
{
	public Task<UserDTO?> GetUser(int userId);
	public Task<IEnumerable<UserDTO>> GetUsers(int page, int limit);
	public Task UpdateUserAsync(UserDTO userDTO);
	public Task DeleteUserAsync(int userId);

}