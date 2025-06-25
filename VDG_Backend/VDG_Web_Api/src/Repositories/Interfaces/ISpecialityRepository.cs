using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface ISpecialityRepository
    {
        public Task<int> AddSpecialityAsyc(string name);
        public Task DeleteSpecialityAsync(int specialityId);
        public Task<Speciality> GetSpecialityAsync(int specialityId);
    }
}
