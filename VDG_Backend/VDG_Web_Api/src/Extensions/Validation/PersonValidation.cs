using VDG_Web_Api.src.DTOs.PersonDTOs;

namespace VDG_Web_Api.src.Extensions.Validation;

public static class PersonValidation
{
	public static bool IsValidPerson(this PersonProfileDTO person)
	{
		return !string.IsNullOrEmpty(person.FirstName);

	}
}