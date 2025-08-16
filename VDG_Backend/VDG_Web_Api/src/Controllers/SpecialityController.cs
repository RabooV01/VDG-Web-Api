using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = nameof(UserRole.Admin))]
	public class SpecialityController : ControllerBase
	{
		private readonly ISpecialityService _specialityService;

		public SpecialityController(ISpecialityService specialityService)
		{
			_specialityService = specialityService;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult> GetAll()
		{
			try
			{
				var specs = await _specialityService.GetSpecialities();
				return Ok(specs);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult> Add(string speciality)
		{
			try
			{
				await _specialityService.AddSpeciality(speciality);
				return Created();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{specId}")]
		public async Task<ActionResult> Delete(int specId)
		{
			try
			{
				await _specialityService.DeleteSpeciality(specId);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
