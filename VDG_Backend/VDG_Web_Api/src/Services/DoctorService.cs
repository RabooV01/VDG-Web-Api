using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
	public class DoctorService : IDoctorService
	{
		private readonly IDoctorRepository _doctorRepository;
		private readonly IUserService _userService;
		public DoctorService(IDoctorRepository doctorRepository, IUserService userService)
		{
			_doctorRepository = doctorRepository;
			_userService = userService;
		}

		public Task<DoctorDTO> GetDoctorById(int doctorId)
		{
			throw new NotImplementedException();
		}

	}
}
