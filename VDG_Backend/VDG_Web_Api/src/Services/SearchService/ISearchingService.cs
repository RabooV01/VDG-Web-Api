using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.DTOs.FilterDTOs;

namespace VDG_Web_Api.src.Services.SearchService
{
	public interface ISearchingService
	{
		public Task<IEnumerable<DoctorSearchDto>> SearchDoctorAsync(FilterDTO filter);
		public Task<IEnumerable<DoctorSearchDto>> GetByName(string name);
	}
}
