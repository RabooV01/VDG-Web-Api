using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Helpers.Pagination;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(Roles = nameof(UserRole.Admin))]
	public class SupportController : ControllerBase
	{
		private readonly ISupportService supportService;

		public SupportController(ISupportService supportService)
		{
			this.supportService = supportService;
		}

		[HttpGet]
		public async Task<ActionResult<PaginationModel<SupportDto>>> GetSupportMessages(int page = 1, int pageSize = 20, string name = "")
		{
			try
			{
				var supportMessages = await supportService.GetSupportMessages(page, pageSize, name);
				return Ok(supportMessages);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddSupportMessage(string message)
		{
			try
			{
				await supportService.AddMessage(message);
				return Created();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut]
		public async Task<ActionResult> ReviewMessage(int messageId)
		{
			try
			{
				await supportService.ReviewMessage(messageId);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteMessage(int messageId)
		{
			try
			{
				await supportService.DeleteMessage(messageId);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
