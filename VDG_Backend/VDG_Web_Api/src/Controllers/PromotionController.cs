using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.PromotionRequest;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = nameof(UserRole.Admin))]
	public class PromotionController : ControllerBase
	{
		private readonly IPromotionRequestService _promotionRequestService;

		public PromotionController(IPromotionRequestService promotionRequestService)
		{
			_promotionRequestService = promotionRequestService;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> RequestPromotion(AddPromotionRequest request)
		{
			try
			{
				await _promotionRequestService.RequestPromotion(request);
				return Created();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PromotionRequestDto>>> GetRequests(int page = 1, int pageSize = 20, bool onlyPending = false, string? name = null)
		{
			try
			{
				var requests = await _promotionRequestService.GetRequests(page, pageSize, onlyPending, name);
				return Ok(requests);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{requestId}/Approve")]
		public async Task<ActionResult> ApproveRequest(int requestId)
		{
			try
			{
				await _promotionRequestService.ApproveRequest(requestId);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{requestId}/Reject")]
		public async Task<ActionResult> RejectRequest(int requestId)
		{
			try
			{
				await _promotionRequestService.RejectRequest(requestId);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{requestId}")]
		public async Task<ActionResult> DeleteRequest(int requestId)
		{
			try
			{
				await _promotionRequestService.DeleteRequest(requestId);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
