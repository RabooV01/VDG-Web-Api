namespace VDG_Web_Api.src.DTOs.PersonDTOs;

public class PersonProfileDTO
{
	public int PersonId { get; set; }

	public string FirstName { get; set; } = string.Empty;

	public string? LastName { get; set; }

	public string? Phone { get; set; }

	public string? Gender { get; set; }

	public string? PersonalId { get; set; }

	public DateOnly? BirthDate { get; set; }
}