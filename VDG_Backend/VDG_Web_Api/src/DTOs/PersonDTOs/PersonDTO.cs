namespace VDG_Web_Api.src.DTOs.PersonDTOs;

public class PersonDTO 
{
    public int Id { get; set; }
    
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; } = null!;
}