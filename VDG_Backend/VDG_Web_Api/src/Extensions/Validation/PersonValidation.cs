using Microsoft.IdentityModel.Tokens;
using VDG_Web_Api.src.DTOs.PersonDTOs;

namespace VDG_Web_Api.src.Extensions.Validation;

public static class PersonValidation
{
    public static bool IsValidPerson(this PersonDTO person)
    {
        return !person.FirstName.IsNullOrEmpty() && !person.LastName.IsNullOrEmpty();
    }

    public static bool IsValidPerson(this PersonProfileDTO person)
    {
        return !person.FirstName.IsNullOrEmpty() && !person.LastName.IsNullOrEmpty();
    }
}