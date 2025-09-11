using VDG_Web_Api.src.DTOs.PersonDTOs;
using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Services.JWTService;

namespace VDG_Web_Api.src.Mapping
{
	public static class UserMappings
	{
		public static User ToEntity(this UserDTO userDTO)
			=> new()
			{
				Email = userDTO.Email,
				Id = userDTO.UserId,
				Person = userDTO.GetPerson(),
				PersonId = userDTO.PersonId,
				RegisteredAt = userDTO.RegisteredAt,
				Role = Enum.Parse<UserRole>(userDTO.Role, true)
			};

		public static UserAuthData ToAuthData(this User user, int? doctorId)
			=> new()
			{
				Email = user.Email,
				DoctorId = doctorId,
				FirstName = user.Person.FirstName,
				UserId = user.Id,
				LastName = user.Person.LastName,
				Role = user.Role.ToString(),
				ImageUrl = user.ImageUrl
			};

		public static UserDTO ToDto(this User user)
			=> new()
			{
				Email = user.Email,
				UserId = user.Id,
				Role = user.Role.ToString(),
				FirstName = user.Person.FirstName,
				LastName = user.Person.LastName,
				PersonId = user.PersonId,
				Phone = user.Person.Phone,
				RegisteredAt = user.RegisteredAt
			};

		public static Person ToEntity(this PersonDTO personDTO)
			=> new()
			{
				Id = personDTO.PersonId,
				FirstName = personDTO.FirstName,
				LastName = personDTO.LastName,
				Phone = personDTO.Phone
			};

		public static Person GetPerson(this UserDTO userDTO)
			=> new()
			{
				Id = userDTO.PersonId,
				FirstName = userDTO.FirstName,
				LastName = userDTO.LastName,
				Phone = userDTO.Phone
			};

		public static PersonDTO ToDto(this Person person)
			=> new()
			{
				PersonId = person.Id,
				FirstName = person.FirstName,
				LastName = person.LastName,
				Phone = person.Phone
			};

		public static PersonProfileDTO ToPersonProfileDto(this Person person)
			=> new()
			{
				PersonId = person.Id,
				FirstName = person.FirstName,
				LastName = person.LastName,
				Phone = person.Phone,
				BirthDate = person.Birthdate,
				Gender = person.Gender,
				PersonalId = person.PersonalId
			};
		public static Person ToEntity(this PersonProfileDTO personProfileDTO)
			=> new()
			{
				FirstName = personProfileDTO.FirstName,
				LastName = personProfileDTO.LastName,
				Phone = personProfileDTO.Phone,
				Birthdate = personProfileDTO.BirthDate,
				Gender = personProfileDTO.Gender,
				PersonalId = personProfileDTO.PersonalId,
				Id = personProfileDTO.PersonId
			};

		public static User ToEntity(this UserRegister userRegister)
			=> new()
			{
				Email = userRegister.Email,
				ImageUrl = userRegister.ImageUrl,
				PasswordHash = userRegister.Password,
				Person = userRegister.Person.ToEntity(),
				RegisteredAt = DateTime.Now,
			};

		public static UserProfileDTO ToProfileDto(this User user)
			=> new()
			{
				FirstName = user.Person.FirstName,
				LastName = user.Person.LastName,
				Phone = user.Person.Phone,
				PersonId = user.Person.Id,
				PersonalId = user.Person.PersonalId,
				BirthDate = user.Person.Birthdate,
				Gender = user.Person.Gender,
				Email = user.Email,
				Role = user.Role,
				UserId = user.Id,
				ImageUrl = user.ImageUrl,
			};
	}
}
