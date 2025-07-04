using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.DTOs.SpecialityDTOS;
using VDG_Web_Api.src.Models;
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
        public SpecialityDTO MapSpecialityToDTO(Speciality speciality)
        {
            return new SpecialityDTO()
            {
                Id = speciality.Id,
                name = speciality.name
            };
        }
        public DoctorDTO MapDoctorToDTO(Doctor doctor)
        {

            return new DoctorDTO()
            {
                Description = doctor.Description,
                Id = doctor.Id,
                UserId = doctor.UserId,
                User = _userService.MapUserToDto(doctor.User),
                SyndicateId = doctor.SyndicateId,
                SpecialityId = doctor.SpecialityId,
                Speciality = MapSpecialityToDTO(doctor.Speciality)
            };
        }

		public Task<DoctorDTO> GetDoctorById(int doctorId)
		{
			throw new NotImplementedException();
		}
		 public DoctorDTO MapToDoctorDto(Doctor doctor)
        {
            return new DoctorDTO()
            {
                Speciality = doctor.Speciality,
                Id = doctor.Id,
                SpecialityId = doctor.SpecialityId,
                SyndicateId = doctor.SyndicateId,
                UserId = doctor.UserId
            };
        }


    }
}
