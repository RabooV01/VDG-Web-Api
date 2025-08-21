using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        public Task Rate(Rating rate);

        public Task UpdateRate(Rating rate);

        public Task DeleteRate(Rating rate);

    }
}
