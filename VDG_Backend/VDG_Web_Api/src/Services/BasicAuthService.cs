using System.Security.Authentication;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Extensions.Validation;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

public class BasicAuthService : IAuthService
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
		if (userRegister == null)
		{
			throw new ArgumentNullException("Something is missing, Double check your credentials and try again.");
		}

		if (!userRegister.IsValidUser() || !userRegister.Person!.IsValidPerson())
		{
			return await Task.FromResult(false);
		}

		try
		{
			var user = new User()
			{
				Email = userRegister.Email,
				Person = _userService.MapPersonDtoToEntity(userRegister.Person!),
				PasswordHash = userRegister.Password,
				Role = "User",
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
		var user = await _userRepository.GetByEmail(userLogin.Email);
		if (user == null)
		{
			throw new ArgumentException("Invalid email or password");
		}

		if (!userLogin.Password.Equals(user.PasswordHash))
		{
			throw new ArgumentException("Invalid email or password");
		}

		return new()
		{
			Id = user.Id,
			Email = user.Email,
			Role = user.Role,
			Person = _userService.MapPersonToDto(user.Person!)
		};
	}
}
