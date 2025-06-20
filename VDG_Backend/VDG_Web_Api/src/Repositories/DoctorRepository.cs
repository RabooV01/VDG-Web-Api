using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly VdgDbDemoContext _context;
        public DoctorRepository(VdgDbDemoContext context)
        {
            _context = context;
        }

        public async Task<int> AddDoctorAsync(Doctor doctor)
        {
            try
            {
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
                return doctor.Id;
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public Task<bool> AddDoctorAsync(int userId, string syndicateId, int specialityId)
        {
            Doctor doctor = new Doctor()
            {
                UserId = userId,
                SyndicateId = syndicateId,
                SpecialityId = specialityId
            };
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

        }

        public Task DeleteDoctorAsync(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor?> GetDoctorByNameAsync(string fullName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDoctorAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
