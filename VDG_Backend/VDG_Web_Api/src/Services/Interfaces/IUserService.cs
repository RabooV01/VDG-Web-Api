using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IUserService
{
    public Task<UserDTO?> GetUser(int userId);
    public Task<IEnumerable<UserDTO>> GetUsers(int page, int limit);
    public Task UpdateUserAsync(PersonProfileDTO userDTO, int userId);
    public Task DeleteUserAsync(int userId);
    public Task<bool> AddUser(UserRegister userRegister);
    public Task<UserProfileDTO> LoadUserProfile(int userId);
    public Task UpdateUserImageAsync(int userId, string imageUrl);
    public Task<string?> GetUserImageAsync(int userId);
}