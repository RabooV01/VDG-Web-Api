using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorDTO>> GetAllDoctors(int page, int pageSize, int? specialityId, string? name)
        {
            try
            {
                var doctors = await _doctorRepository.GetDoctors(page, pageSize, specialityId, name);
                return doctors.Select(d => d.ToDto());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task AddDoctor(AddDoctorDTO doctorDTO)
        {
            try
            {
                var doctor = await _doctorRepository.AddDoctorAsync(doctorDTO.ToEntity());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteDoctor(int doctorId)
        {
            try
            {
                await _doctorRepository.DeleteDoctorAsync(doctorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DoctorDTO> GetDoctorById(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);

            if (doctor == null)
            {
                throw new KeyNotFoundException("No such doctor");
            }

            return doctor.ToDto();
        }

        public async Task UpdateDoctorConsultationSettings(DoctorSettings doctorSettings, int doctorId)
        {
            try
            {
                await _doctorRepository.UpdateDoctorSettings(doctorId, doctorSettings.TicketOption, doctorSettings.TicketCost);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateDoctorDescription(int doctorId, string description)
        {
            try
            {
                await _doctorRepository.UpdateDoctorDescription(description, doctorId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DoctorRatingDto>> GetTopDoctor()
        {
            try
            {
                return (await _doctorRepository.GetTopTenDoctors()).Select(d => new DoctorRatingDto()
                {
                    DoctorId = d.DoctorId,
                    DoctorName = d.Doctor.User.Person.FirstName + " " + d.Doctor.User.Person.LastName,
                    Rating = d.Rating
                });
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
