namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        public Task GetById(int id);
        public Task GetByName(string fullName);
        public Task<>
        public Task DeleteById(int id);


    }
}
