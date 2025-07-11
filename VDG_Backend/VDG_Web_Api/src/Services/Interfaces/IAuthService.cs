using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IAuthService
{
	public Task<string> AuthenticateAsync(UserLogin userRegister);
	public Task<User?> ValidateUser(UserLogin userLogin);
}