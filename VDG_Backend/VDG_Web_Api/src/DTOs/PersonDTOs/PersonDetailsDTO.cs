namespace VDG_Web_Api.src.DTOs.PersonDTOs;

public class PersonProfileDTO
{
    public int? Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Gender { get; set; }

    public string? PersonalId { get; set; }

    public DateOnly? BirthDate { get; set; }
}