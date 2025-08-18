using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ClinicController : ControllerBase
	{
		private readonly IVirtualClinicService _clinicService;

		public ClinicController(IVirtualClinicService clinicService)
		{
			_clinicService = clinicService;
		}

		[HttpGet("Doctor/{doctorId}")]
		public async Task<ActionResult<VirtualClinicInProfileDTO>> GetDoctorClinics(int doctorId)
		{
			try
			{
				var clinics = await _clinicService.GetClinicsByDoctorId(doctorId);
				return Ok(clinics);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{clinicId}")]
		public async Task<ActionResult<VirtualClinicDTO>> GetClinicById(int clinicId)
		{
			try
			{
				var clinic = await _clinicService.GetClinicById(clinicId);
				return Ok(clinic);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{clinicId}/Worktimes")]
		public async Task<ActionResult<ClinicWorkTimeDTO>> GetClinicWorkTimes(int clinicId)
		{
			try
			{
				var worktimes = await _clinicService.GetClinicWorkTimes(clinicId);
				return Ok(worktimes);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddClinic(AddVirtualClinicDTO clinicDTO)
		{
			try
			{
				await _clinicService.AddClinic(clinicDTO);
				return Created();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost("Worktime")]
		public async Task<ActionResult> AddWorktime(ClinicWorkTimeDTO clinicWorkTime)
		{
			try
			{
				await _clinicService.AddClinicWorkTime(clinicWorkTime);
				return Created();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut]
		public async Task<ActionResult> UpdateClinic(UpdateVirtualClinicDTO clinicDTO)
		{
			try
			{
				await _clinicService.UpdateClinic(clinicDTO);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{clinicId}")]
		public async Task<ActionResult> DeleteClinic(int clinicId)
		{
			try
			{
				await _clinicService.DeleteClinic(clinicId);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("Worktime/{workTimeId}")]
		public async Task<ActionResult> DeleteWorkTime(int workTimeId)
		{
			try
			{
				await _clinicService.RemoveClinicWorkTime(workTimeId);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
