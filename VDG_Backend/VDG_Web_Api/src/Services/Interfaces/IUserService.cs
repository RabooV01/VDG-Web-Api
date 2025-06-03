using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IUserService
{
    public Task<UserDTO?> GetByIdAsync(int userId);
}