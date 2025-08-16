using VDG_Web_Api.src.DTOs.SpecialityDTOS;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
	public class SpecialityService : ISpecialityService
	{
		private readonly ISpecialityRepository _specialityRepository;

		public SpecialityService(ISpecialityRepository specialityRepository)
		{
			_specialityRepository = specialityRepository;
		}

		public async Task AddSpeciality(string speciality)
		{
			try
			{
				await _specialityRepository.AddSpecialityAsyc(speciality);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task DeleteSpeciality(int specialityId)
		{
			try
			{
				await _specialityRepository.DeleteSpecialityAsync(specialityId);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<IEnumerable<SpecialityDTO>> GetSpecialities()
		{
			try
			{
				var specialities = await _specialityRepository.GetSpecialities();
				return specialities.Select(s => s.ToDto());
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
