using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<DoctorDTO> GetDoctorById(int doctorId);
        public Task AddDoctor(AddDoctorDTO doctorDTO);
        public Task DeleteDoctor(int doctorId);
        public Task UpdateDoctorDescription(int doctorId, string description);
        public Task UpdateDoctorConsultationSettings(DoctorSettings doctorSettings, int doctorId);

        public Task<IEnumerable<Doctor>> GetTopDoctor(int cnt = 10);

        public Task<IEnumerable<DoctorDTO>> GetAllDoctors(int page, int pageSize, int? specialityId = null, string? name = null);
    }
}
