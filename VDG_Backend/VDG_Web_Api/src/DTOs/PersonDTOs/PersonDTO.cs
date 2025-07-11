namespace VDG_Web_Api.src.DTOs.PersonDTOs;

public class PersonDTO
{
	public int Id { get; set; }

	public string FirstName { get; set; } = string.Empty;

	public string? LastName { get; set; } = string.Empty;

	public string? Phone { get; set; } = string.Empty;
}