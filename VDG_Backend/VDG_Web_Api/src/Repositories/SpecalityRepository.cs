using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class SpecalityRepository : ISpecialityRepository
    {
        private readonly VdgDbDemoContext _context;
        public SpecalityRepository(VdgDbDemoContext context)
        {
            _context = context;
        }

        public async Task<int> AddSpcialityAsyc(string name)
        {
            try
            {
                var speciality = new Speciality() { name = name };
                await _context.Specialities.AddAsync(speciality);
                await _context.SaveChangesAsync();
                return speciality.Id;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to ِadd speciality,[{ex.Message}]", ex);
            }
        }

        public async Task DeleteSpcialityAsync(int specialityId)
        {
            var speciality = await _context.Specialities.FindAsync(specialityId);
            if (speciality == null)
            {
                throw new KeyNotFoundException("Speciality is not found");
            }

            try
            {
                _context.Specialities.Remove(speciality);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to ِremove speciality,[{ex.Message}]", ex);
            }

        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsBySpcilityAsync(int spcialityId)
        {
            // ممكن لقدام نبعت actoin لنفلتر الدكاترة اللي من هاد التخصص
            try
            {
                return await _context.Doctors.Where(d => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to display doctors , Error: {ex.Message}", ex);
            }

        }



    }
}
