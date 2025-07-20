using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.DTOs.UserDTOs;

public class UserProfileDTO : PersonProfileDTO
{
	public int? UserId { get; set; }

	public string Email { get; set; } = null!;

	public UserRole Role { get; set; }
}