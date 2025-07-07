using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping
{
	public static class UserMappings
	{
		public static User ToEntity(this UserDTO userDTO)
			=> new()
			{
				Email = userDTO.Email,
				Id = userDTO.Id,
				Person = userDTO.Person.ToEntity(),
				PersonId = userDTO.Person.Id,
				Role = userDTO.Role
			};

		public static UserDTO ToDto(this User user)
			=> new()
			{
				Email = user.Email,
				Id = user.Id,
				Role = user.Role,
				Person = user.Person.ToDto()
			};

		public static Person ToEntity(this PersonDTO personDTO)
			=> new()
			{
				Id = personDTO.Id,
				FirstName = personDTO.FirstName,
				LastName = personDTO.LastName,
				Phone = personDTO.Phone
			};

		public static PersonDTO ToDto(this Person person)
			=> new()
			{
				Id = person.Id,
				FirstName = person.FirstName,
				LastName = person.LastName,
				Phone = person.Phone
			};

		public static PersonProfileDTO ToPersonProfileDto(this Person person)
			=> new()
			{
				Id = person.Id,
				FirstName = person.FirstName,
				LastName = person.LastName,
				Phone = person.Phone,
				BirthDate = person.Birthdate,
				Gender = person.Gender,
				PersonalId = person.PersonalId
			};
	}
}
