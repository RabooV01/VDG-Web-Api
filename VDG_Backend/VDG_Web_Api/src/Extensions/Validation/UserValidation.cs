using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.Extensions.Validation;

public static class UserValidation
{
	private static readonly int minimumPasswordLengthAllowed = 8;

	public static bool IsValidUser(this UserRegister user) =>
		!string.IsNullOrEmpty(user.Email) &&
		!string.IsNullOrEmpty(user.Password) &&
		user.Person.IsValidPerson() &&
		user.Password.Length >= minimumPasswordLengthAllowed;
}