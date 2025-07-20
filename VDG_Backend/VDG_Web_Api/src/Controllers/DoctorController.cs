using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "Doctor-Admin")]
	public class DoctorController : ControllerBase
	{
		private readonly IDoctorService _doctorService;
		private readonly IClaimService _claimService;

		public DoctorController(IDoctorService doctorService, IClaimService claimService)
		{
			_doctorService = doctorService;
			_claimService = claimService;
		}

		[HttpGet("{doctorId}")]
		public async Task<ActionResult> GetDoctor(int doctorId)
		{
			try
			{
				var doctor = await _doctorService.GetDoctorById(doctorId);

				if (doctor.UserId != _claimService.GetCurrentUserId() && !_claimService.GetCurrentUserRole().Equals(UserRole.Admin))
				{
					return Unauthorized();
				}

				return Ok(doctor);
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpPut]
		public async Task<ActionResult> UpdateDescription(string description)
		{
			try
			{
				await _doctorService.UpdateDoctorDescription(_claimService.GetCurrentUserId(), description);
				return NoContent();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpPut("Settings")]
		public async Task<ActionResult> UpdateDoctorSettings(DoctorSettings doctorSettings)
		{
			try
			{
				await _doctorService.UpdateDoctorConsultationSettings(doctorSettings, _claimService.GetCurrentUserId());
				return NoContent();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpDelete("{doctorId}")]
		public async Task<ActionResult> DeleteDoctor(int doctorId)
		{
			try
			{
				await _doctorService.DeleteDoctor(doctorId);
				return NoContent();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Authorize(Roles = $"{nameof(UserRole.Admin)}")]
		public async Task<ActionResult> PromoteUserToDoctor(AddDoctorDTO doctorDTO)
		{
			try
			{
				await _doctorService.AddDoctor(doctorDTO);
				return Created();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
	}
}
