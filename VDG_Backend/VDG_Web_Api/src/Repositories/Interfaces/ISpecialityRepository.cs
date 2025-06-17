using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface ISpecialityRepository
    {
        public Task<int> AddSpcialityAsyc(string name);
        public Task DeleteSpcialityAsync(int specialityId);
        public Task<IEnumerable<Doctor>> GetAllDoctorsBySpcilityAsync(int spcialityId);
    }
}
