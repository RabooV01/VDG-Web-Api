using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Extensions.Validation;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly string FailedUserOperationMessage = "User operation failed due to unexpected error";
	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	private string UserInvalidOperationErrorMessage(string operationName) =>
		$"{operationName} {FailedUserOperationMessage}";


	public async Task DeleteUserAsync(int userId)
	{
		try
		{
			await _userRepository.DeleteUserAsync(userId);
		}
		catch (KeyNotFoundException ex)
		{
			throw new InvalidOperationException(ex.Message, ex);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"{UserInvalidOperationErrorMessage(nameof(DeleteUserAsync))}, {ex.Message}", ex);
		}
	}

	public async Task<UserDTO?> GetUser(int userId)
	{

		var user = await _userRepository.GetById(userId) ?? throw new KeyNotFoundException("User has not been found");
		try
		{
			return user.ToDto();
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed mapping user, Error: {ex.Message}", ex);
		}
	}

	public async Task<IEnumerable<UserDTO>> GetUsers(int page, int limit)
	{
		try
		{
			var users = await _userRepository.GetUsers(page, limit);
			return users.Select(u => u.ToDto());
		}
		catch (Exception ex)
		{
			throw new Exception($"{UserInvalidOperationErrorMessage(nameof(GetUsers))}, {ex.Message}", ex);
		}
	}

	public async Task UpdateUserAsync(UserDTO userDTO)
	{
		User user = await _userRepository.GetById(userDTO.Id) ?? throw new ArgumentException("Invalid User");
		try
		{
			await _userRepository.UpdateUserAsync(user);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Update Failed. Error {ex.Message}", ex);
		}
	}

	public async Task<bool> AddUser(UserRegister userRegister)
	{
		try
		{
			if (!userRegister.IsValidUser())
			{
				return false;
			}

			await _userRepository.AddUserAsync(userRegister.ToEntity());

			return true;
		}
		catch (Exception)
		{
			throw;
		}
	}
}