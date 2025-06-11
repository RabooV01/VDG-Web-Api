using VDG_Web_Api.src.DTOs.PersonDTOs;

namespace VDG_Web_Api.src.DTOs.UserDTOs;

public class UserProfileDTO
{
    public PersonProfileDTO @Person { get; set; } = null!;

    public int? Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Role { get; set; }
}