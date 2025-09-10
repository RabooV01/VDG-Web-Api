using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        public Task Rate(Rating rate);

        public Task UpdateRate(int rateId, double serv, double wait, double act);

        public Task DeleteRate(int rate);

        public Task<IEnumerable<Rating>> GetRate(int id);

    }
}
