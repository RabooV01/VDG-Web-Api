using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping;

public static class DoctorMapping
{
	public static Doctor ToEntity(this DoctorDTO doctorDTO)
		=> new()
		{
			Id = doctorDTO.DoctorId,
			UserId = doctorDTO.UserId,
			SpecialityId = doctorDTO.SpecialityId,
			User = doctorDTO.GetUser()
		};

	public static DoctorDTO ToDto(this Doctor doctor)
		=> new()
		{
			DoctorId = doctor.Id,
			SpecialityId = doctor.SpecialityId,
			Speciality = doctor.Speciality.name,
			PersonId = doctor.User.PersonId,
			FirstName = doctor.User.Person.FirstName,
			LastName = doctor.User.Person.LastName,
			Phone = doctor.User.Person.Phone,
			UserId = doctor.UserId,
			Email = doctor.User.Email,
			Role = doctor.User.Role,
			Description = doctor.Description
		};

	public static User GetUser(this DoctorDTO doctorDTO)
		=> new()
		{
			Id = doctorDTO.UserId,
			PersonId = doctorDTO.PersonId,
			Email = doctorDTO.Email,
			Role = doctorDTO.Role,
			Person = doctorDTO.GetPerson()
		};

	public static Person GetPerson(this DoctorDTO doctorDTO)
		=> new()
		{
			FirstName = doctorDTO.FirstName,
			LastName = doctorDTO.LastName,
			Phone = doctorDTO.Phone,
			Id = doctorDTO.PersonId
		};
}
