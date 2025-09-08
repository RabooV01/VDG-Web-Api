using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class RatingRepositroy : IRatingRepository
    {
        private readonly VdgDbDemoContext _context;

        public RatingRepositroy(VdgDbDemoContext context)
        {
            _context = context;
        }


        public async Task<Rating> GetRate(int id)
        {
            try
            {
                return await _context.Ratings.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException($"Error while retrieving data. {ex.Message}", ex);
            }
        }

        public async Task Rate(Rating rating)
        {
            try
            {
                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }

        public async Task UpdateRate(Rating rating)
        {
            try
            {
                _context.Ratings.Update(rating);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }
        public async Task DeleteRate(Rating rating)
        {
            try
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }
    }
}
