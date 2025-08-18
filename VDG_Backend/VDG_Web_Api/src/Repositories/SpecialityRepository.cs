using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
	public class SpecialityRepository : ISpecialityRepository
	{
		private readonly VdgDbDemoContext _context;
		public SpecialityRepository(VdgDbDemoContext context)
		{
			_context = context;
		}

		public async Task<int> AddSpecialityAsyc(string name)
		{
			try
			{
				var speciality = new Speciality() { Name = name };
				await _context.Specialities.AddAsync(speciality);
				await _context.SaveChangesAsync();
				return speciality.Id;

			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Unable to ِadd speciality,error:[{ex.Message}]", ex);
			}
		}

		public async Task DeleteSpecialityAsync(int specialityId)
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
				throw new InvalidOperationException($"Unable to ِremove speciality,error:[{ex.Message}]", ex);
			}

		}

		public async Task<IEnumerable<Speciality>> GetSpecialities()
		{
			try
			{
				var specialities = await _context.Specialities.ToListAsync();
				return specialities;
			}
			catch (Exception e)
			{
				throw new Exception("Error while retrieving data.", e);
			}
		}

		public async Task<Speciality> GetSpecialityAsync(int specialityId)
		{
			var speciality = await _context.Specialities.FindAsync(specialityId);
			if (speciality == null)
				throw new KeyNotFoundException("Speciality is not found");

			return speciality;
		}

	}
}
