using VDG_Web_Api.src.DTOs.PromotionRequest;

namespace VDG_Web_Api.src.Services.Interfaces
{
	public interface IPromotionRequestService
	{
		public Task ApproveRequest(int requestId);
		public Task DeleteRequest(int requestId);
		public Task<IEnumerable<PromotionRequestDto>> GetRequests(int page, int pageSize, bool onlyPending, string? name);
		public Task RejectRequest(int requestId);
		public Task RequestPromotion(AddPromotionRequest request);
	}
}
