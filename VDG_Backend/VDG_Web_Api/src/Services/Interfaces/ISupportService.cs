using VDG_Web_Api.src.DTOs;
using VDG_Web_Api.src.Helpers.Pagination;

namespace VDG_Web_Api.src.Services.Interfaces
{
	public interface ISupportService
	{
		public Task<PaginationModel<SupportDto>> GetSupportMessages(int page, int pageSize, string name);
		public Task AddMessage(string message);
		public Task DeleteMessage(int messageId);
		public Task ReviewMessage(int messageId);
	}
}
