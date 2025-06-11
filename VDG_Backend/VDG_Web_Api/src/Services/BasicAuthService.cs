using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

public class BasicAuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    BasicAuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<bool> AuthenticateAsync(UserRegister userRegister)
    {
        throw new NotImplementedException();
    }
}
