using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services;

public class VirtualClinicService : IVirtualClinicService
{
	private readonly IVirtualClinicRepository _clinicRepository;
	private readonly IClaimService _claimService;
	private readonly IDoctorRepository _doctorRepository;
	public VirtualClinicService(IVirtualClinicRepository clinicRepository, IDoctorService doctorService, IClaimService claimService, IDoctorRepository doctorRepository)
	{
		_clinicRepository = clinicRepository;
		_claimService = claimService;
		_doctorRepository = doctorRepository;
	}

	private List<ClinicWorkTime> GenerateWorkingTime(WorkTimeInitialize workTimeInit)
	{
		IEnumerable<ClinicWorkTime> workTimes = new List<ClinicWorkTime>();
		return Enumerable.Range(0, 7)
			.Where(day => !workTimeInit.Holidays.Contains((DayOfWeek)day))
			.GroupJoin(workTimes, day => day, workTime => (int)workTime.DayOfWeek,
			(day, workTime) => new ClinicWorkTime()
			{
				DayOfWeek = (DayOfWeek)day,
				ClinicId = workTimeInit.ClinicId,
				StartWorkHours = workTimeInit.StartWorkHours,
				EndWorkHours = workTimeInit.EndWorkHours
			}).ToList();
	}

	public async Task AddClinic(AddVirtualClinicDTO clinic)
	{
		var doctor = await _doctorRepository.GetDoctorByUserId(_claimService.GetCurrentUserId());

		if (doctor == null && !_claimService.GetCurrentUserRole().Equals(UserRole.Admin))
		{
			throw new UnauthorizedAccessException();
		}

		if (doctor!.Id != clinic.DoctorId && !_claimService.GetCurrentUserRole().Equals(UserRole.Admin))
		{
			clinic.DoctorId = doctor.Id;
		}

		try
		{
			var virtualClinic = clinic.ToEntity();
			virtualClinic.WorkTimes = GenerateWorkingTime(clinic.WorkTime);
			await _clinicRepository.AddClinic(virtualClinic);
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Couldn't add clinic", e);
		}

	}

	public async Task AddClinicWorkTime(ClinicWorkTimeDTO workTimeDTO)
	{

		try
		{
			ClinicWorkTime workTime = workTimeDTO.ToEntity();
			await _clinicRepository.AddClinicWorkTime(workTime);
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Clinic has not been added, Error: {e.Message}", e);
		}
	}

	public async Task DeleteClinic(int clinicId)
	{
		try
		{
			await _clinicRepository.DeleteClinic(clinicId);
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"clinic has not been deleted, Error: {e.Message}", e);
		}
	}

	public async Task<VirtualClinicDTO> GetClinicById(int clinicId)
	{
		VirtualClinic? clinic = await _clinicRepository.GetClinicById(clinicId);

		if (clinic == null)
		{
			throw new ArgumentNullException(nameof(clinic), "invalid clinic");
		}

		return clinic.ToDto();
	}

	public async Task<IEnumerable<ClinicWorkTimeDTO>> GetClinicWorkTimes(int clinicId)
	{
		var workTimes = await _clinicRepository.GetClinicWorkTimes(clinicId);
		return workTimes.Select(wt => wt.ToDto());
	}

	public async Task RemoveClinicWorkTime(int workTimeId)
	{
		try
		{
			await _clinicRepository.RemoveClinicWorkTime(workTimeId);
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Unable to remove clinic worktime.", e);
		}
	}

	public async Task UpdateClinic(UpdateVirtualClinicDTO clinicDTO)
	{
		try
		{
			VirtualClinic clinic = clinicDTO.ToEntity();

			await _clinicRepository.UpdateClinic(clinic);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<IEnumerable<VirtualClinicInProfileDTO>> GetClinicsByDoctorId(int doctorId)
	{
		try
		{
			var clinics = await _clinicRepository.GetClinicsByDoctorId(doctorId);

			return clinics.Select(c => c.ToClinicInProfileDto());
		}
		catch (Exception)
		{
			throw;
		}
	}

}
