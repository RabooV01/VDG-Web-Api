using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.DTOs.FilterDTOs;
using VDG_Web_Api.src.Helpers.Pagination;

namespace VDG_Web_Api.src.Services.SearchService
{
	public interface ISearchingService
	{
		public Task<PaginationModel<DoctorSearchDto>> SearchDoctorAsync(FilterDTO filter, int page, int pageSize);
		public Task<IEnumerable<DoctorSearchDto>> GetByName(string name);
	}
}
