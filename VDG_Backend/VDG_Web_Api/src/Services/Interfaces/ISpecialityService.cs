using VDG_Web_Api.src.DTOs.SpecialityDTOS;

namespace VDG_Web_Api.src.Services.Interfaces
{
	public interface ISpecialityService
	{
		public Task<IEnumerable<SpecialityDTO>> GetSpecialities();
		public Task DeleteSpeciality(int specialityId);
		public Task AddSpeciality(string speciality);
	}
}
