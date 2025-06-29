using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
	public class DoctorService : IDoctorService
	{
		public DoctorService() { }

		public Task<DoctorDTO> GetDoctorById(int doctorId)
		{
			throw new NotImplementedException();
		}
		public DoctorDTO MapToDoctorDto(Doctor doctor)
		{
			return new DoctorDTO()
			{
				Speciality = doctor.Speciality.name,
				Id = doctor.Id,
				SpecialityId = doctor.SpecialityId,
				SyndicateId = doctor.SyndicateId,
				UserId = doctor.UserId
			};
		}
	}
}
