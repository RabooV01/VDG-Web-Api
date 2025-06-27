using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services;

public class VirtualClinicService : IVirtualClinicService
{
	private readonly IVirtualClinicRepository _clinicRepository;

	public VirtualClinicService(IVirtualClinicRepository clinicRepository)
	{
		_clinicRepository = clinicRepository;
	}

	private VirtualClinicDTO MapToDTO(VirtualClinic clinic)
		=> new()
		{
			Id = clinic.Id,
			DoctorId = clinic.DoctorId,
			Status = clinic.Status,
			Location = clinic.Location,
			AvgService = clinic.AvgService,
			Doctor = clinic.Doctor,
			PreviewCost = clinic.PreviewCost
		};

	private VirtualClinic MapToEntity(VirtualClinicDTO clinicDTO)
		=> new()
		{
			Id = clinicDTO.Id,
			DoctorId = clinicDTO.DoctorId,
			Location = clinicDTO.Location,
			Status = clinicDTO.Status,
			PreviewCost = clinicDTO.PreviewCost,
			AvgService = clinicDTO.AvgService,
			Doctor = clinicDTO.Doctor
		};

	public async Task AddClinic(VirtualClinicDTO clinic)
	{
		if (clinic.StartWorkHours == null || clinic.EndWorkHours == null)
		{
			throw new ArgumentNullException("Must provide initial workTime range");
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

			await _clinicRepository.AddClinic(virtualClinic, workTime);
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

	public Task<IEnumerable<ClinicWorkTime>> GetClinicWorkTimes(int clinicId)
	{
		throw new NotImplementedException();
	}

	public Task RemoveClinicWorkTime(int workTimeId)
	{
		throw new NotImplementedException();
	}

	public Task UpdateClinic(VirtualClinicDTO clinicDTO)
	{
		throw new NotImplementedException();
	}
}
