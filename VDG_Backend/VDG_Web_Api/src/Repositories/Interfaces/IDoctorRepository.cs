using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<bool> AddDoctorAsync(int userId, string syndicateId, int specialityId);
        public Task DeleteDoctorAsync(int doctorId);
        public Task UpdateDoctorAsync(Doctor doctor);
        public Task<Doctor?> GetDoctorByIdAsync(int id);
        public Task<Doctor?> GetDoctorByNameAsync(string fullName);

    }
}
