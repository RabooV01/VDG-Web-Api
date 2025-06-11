using Microsoft.AspNetCore.Authentication;
using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IAuthService
{
    public Task<bool> AuthenticateAsync(UserRegister userRegister);
    public Task<bool> ValidateUser(UserLogin userLogin);
}