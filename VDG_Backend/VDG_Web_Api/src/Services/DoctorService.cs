using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
	public class DoctorService : IDoctorService
	{
		private readonly IUserService _userService;
		public DoctorService(IUserService userService)
		{
			_userService = userService;
		}

		public Task<DoctorDTO> GetDoctorById(int doctorId)
		{
			throw new NotImplementedException();
		}
		public DoctorDTO MapToDoctorDto(Doctor doctor)
		{
			return new DoctorDTO()
			{
				Speciality = doctor.Speciality?.name ?? string.Empty,
				Id = doctor.Id,
				SpecialityId = doctor.SpecialityId,
				SyndicateId = doctor.SyndicateId,
				UserId = doctor.UserId,
				User = _userService.MapUserToDto(doctor.User ?? new())
			};
		}
	}
}
