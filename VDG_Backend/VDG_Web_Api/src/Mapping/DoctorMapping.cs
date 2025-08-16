using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping;

public static class DoctorMapping
{
	public static Doctor ToEntity(this AddDoctorDTO doctorDTO)
		=> new()
		{
			UserId = doctorDTO.UserId,
			Description = doctorDTO.Description,
			SpecialityId = doctorDTO.SpecialityId,
			SyndicateId = doctorDTO.SyndicateId,
			TicketCost = doctorDTO.TicketCost,
			TicketOption = doctorDTO.TicketOptions
		};
	public static Doctor ToEntity(this DoctorDTO doctorDTO)
		=> new()
		{
			Id = doctorDTO.DoctorId,
			UserId = doctorDTO.UserId,
			SpecialityId = doctorDTO.SpecialityId,
			User = doctorDTO.GetUser(),
			TicketCost = doctorDTO.TicketCost,
			TicketOption = Enum.Parse<TicketOptions>(doctorDTO.TicketOption)
		};

	public static DoctorInfo ToInfo(this Doctor doctor)
		=> new()
		{
			Id = doctor.Id,
			FirstName = doctor.User.Person.FirstName,
			LastName = doctor.User.Person.LastName ?? string.Empty,
			Speciality = doctor.Speciality.Name
		};

	public static DoctorDTO ToDto(this Doctor doctor)
		=> new()
		{
			DoctorId = doctor.Id,
			SpecialityId = doctor.SpecialityId,
			Speciality = doctor.Speciality.Name,
			PersonId = doctor.User.PersonId,
			FirstName = doctor.User.Person.FirstName,
			LastName = doctor.User.Person.LastName,
			Phone = doctor.User.Person.Phone,
			UserId = doctor.UserId,
			Email = doctor.User.Email,
			Role = doctor.User.Role.ToString(),
			Description = doctor.Description,
			TicketCost = doctor.TicketCost,
			TicketOption = doctor.TicketOption.ToString()
		};

	public static User GetUser(this DoctorDTO doctorDTO)
		=> new()
		{
			Id = doctorDTO.UserId,
			PersonId = doctorDTO.PersonId,
			Email = doctorDTO.Email,
			Role = Enum.Parse<UserRole>(doctorDTO.Role, true),
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
