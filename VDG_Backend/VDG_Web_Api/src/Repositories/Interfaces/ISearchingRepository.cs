using VDG_Web_Api.src.DTOs.FilterDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface ISearchingRepository
    {
        public Task<IEnumerable<Doctor>> SearchDoctorAsync(FilterDTO filter);


    }
}
