using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services;

public class VirtualClinicService : IVirtualClinicService
{
	private readonly IVirtualClinicRepository _clinicRepository;
	private readonly IDoctorService _doctorService;

	public VirtualClinicService(IVirtualClinicRepository clinicRepository, IDoctorService doctorService)
	{
		_clinicRepository = clinicRepository;
		_doctorService = doctorService;
	}

	

	public async Task AddClinic(VirtualClinicDTO clinic)
	{
		if (clinic.StartWorkHours > clinic.EndWorkHours)
		{
			throw new ArgumentNullException("invalid worktimes");
		}

		try
		{
			ClinicWorkTime workTime = new() { StartWorkHours = clinic.StartWorkHours, EndWorkHours = clinic.EndWorkHours };

			VirtualClinic virtualClinic = new()
			{
				DoctorId = clinic.DoctorId,
				AvgService = clinic.AvgService,
				Location = clinic.Location,
				PreviewCost = clinic.PreviewCost
			};

			await _clinicRepository.AddClinic(virtualClinic);
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Couldn't add clinic, Error: {e.Message}", e);
		}

	}

	public async Task AddClinicWorkTime(ClinicWorkTimeDTO workTimeDTO)
	{
		ClinicWorkTime workTime = new()
		{
			ClinicId = workTimeDTO.ClinicId,
			StartWorkHours = workTimeDTO.StartWorkHours,
			EndWorkHours = workTimeDTO.EndWorkHours
		};

		try
		{
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
			throw new ArgumentNullException();
		}

		var clinicDTO = MapToDTO(clinic);

		return clinicDTO;
	}

	public async Task<IEnumerable<ClinicWorkTimeDTO>> GetClinicWorkTimes(int clinicId)
	{
		var workTimes = await _clinicRepository.GetClinicWorkTimes(clinicId);
		return workTimes.Select(wt => MapToDTO(wt));
	}

	public async Task RemoveClinicWorkTime(int workTimeId)
	{
		try
		{
			await _clinicRepository.RemoveClinicWorkTime(workTimeId);	
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Unable to remove clinic worktime. Error {e.Message}", e);
		}
	}

	public async Task UpdateClinic(VirtualClinicDTO clinicDTO)
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

	public async Task<VirtualClinicDTO> GetClinicsByDoctorId(int doctorId)
	{
		var clinics = await _clinicRepository.GetClinicsByDoctorId(doctorId);
		throw new NotImplementedException();
	}
}
