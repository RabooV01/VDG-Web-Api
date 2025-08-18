using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface IPromotionRequestRepository
	{
		public Task Update(PromotionRequest request);
		public Task<IEnumerable<PromotionRequest>> GetPromotionRequests(int page, int pageSize, bool onlyPending = false, string? name = null);
		public Task Delete(int requestId);
		public Task<PromotionRequest?> GetById(int id);

		public Task RequestPromotion(PromotionRequest request);
	}
}
