using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
    public class DoctorService : IDoctorService
    {
        public DoctorService() { }

        public Task<DoctorDTO> GetDoctorById(string doctorId)
        {
            throw new NotImplementedException();
        }
    }
}
