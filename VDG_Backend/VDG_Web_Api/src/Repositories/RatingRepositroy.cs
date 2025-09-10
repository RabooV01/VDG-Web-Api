using Microsoft.EntityFrameworkCore;
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


        public async Task<IEnumerable<Rating>> GetRate(int id)
        {
            try
            {
                return await _context.Ratings.Include(u => u.User)
                    .ThenInclude(u => u.Person)
                    .Where(r => r.DoctorId == id).ToListAsync();
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

        public async Task UpdateRate(int rating, double sev, double wait, double act)
        {
            try
            {
                await _context.Ratings.Where(r => r.Id == rating)
                    .ExecuteUpdateAsync(r => r.SetProperty(p => p.AvgService, sev).SetProperty(p => p.AvgWait, wait).SetProperty(p => p.Act, act));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }
        public async Task DeleteRate(int rating)
        {
            try
            {
                await _context.Ratings.Where(r => r.Id == rating).ExecuteDeleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }
    }
}
