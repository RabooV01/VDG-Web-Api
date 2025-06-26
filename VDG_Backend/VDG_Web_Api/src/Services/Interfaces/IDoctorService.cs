using VDG_Web_Api.src.DTOs.DoctorDTOs;

namespace VDG_Web_Api.src.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<DoctorDTO> GetDoctorById(string doctorId);
    }
}
