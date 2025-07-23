using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Services.JWTService;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IAuthService
{
	public Task<AuthResponse> AuthenticateAsync(UserLogin userRegister);
	public Task<User?> ValidateUser(UserLogin userLogin);
}