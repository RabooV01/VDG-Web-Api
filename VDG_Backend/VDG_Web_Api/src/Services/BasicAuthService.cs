using System.Security.Authentication;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Extensions.Validation;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services;

public class BasicAuthService
{
	private readonly IUserRepository _userRepository;
	private readonly IUserService _userService;

	public BasicAuthService(IUserRepository userRepository, IUserService userService)
	{
		_userRepository = userRepository;
		_userService = userService;
	}

	public async Task<bool> AuthenticateAsync(UserRegister userRegister)
	{
		ArgumentNullException.ThrowIfNull(userRegister);

		if (!userRegister.IsValidUser() || !userRegister.Person!.IsValidPerson())
		{
			return await Task.FromResult(false);
		}

		try
		{
			var user = new User()
			{
				Email = userRegister.Email,
				Person = userRegister.Person.ToEntity(),
				PasswordHash = userRegister.Password,
				Role = UserRole.User
			};

			await _userRepository.AddUserAsync(user);

			return await Task.FromResult(true);
		}
		catch (Exception ex)
		{
			throw new AuthenticationException($"User is not registered on system, Error: {ex.Message}", ex);
		}
	}

	public async Task<UserDTO> ValidateUser(UserLogin userLogin)
	{
		var user = await _userRepository.GetByEmail(userLogin.Email) ?? throw new ArgumentException("Invalid email or password");
		if (!userLogin.Password.Equals(user.PasswordHash))
		{
			throw new ArgumentException("Invalid email or password");
		}

		return user.ToDto();
	}
}