using Microsoft.IdentityModel.Tokens;
using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.Extensions.Validation;

public static class UserValidation
{
	private static readonly int minimumPasswordLengthAllowed = 8;
	public static bool IsValidUser(this UserDTO user) =>
		!user.Email.IsNullOrEmpty() && user.Person.IsValidPerson();

	public static bool IsValidUser(this UserProfileDTO user) =>
		!user.Email.IsNullOrEmpty() && user.Person.IsValidPerson();

	public static bool IsValidUser(this UserRegister user) =>
		!user.Email.IsNullOrEmpty() && !user.Password.IsNullOrEmpty() && user.Person != null && user.Password.Length >= minimumPasswordLengthAllowed;

}