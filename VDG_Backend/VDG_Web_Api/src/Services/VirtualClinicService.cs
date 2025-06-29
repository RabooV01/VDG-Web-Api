using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
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

	public VirtualClinicDTO MapToDTO(VirtualClinic clinic)
		=> new()
		{
			Id = clinic.Id,
			DoctorId = clinic.DoctorId,
			Status = clinic.Status,
			Location = clinic.Location,
			AvgService = clinic.AvgService,
			Doctor = ((DoctorService)_doctorService).MapToDoctorDto(clinic.Doctor ?? new()),
			PreviewCost = clinic.PreviewCost
		};

	public VirtualClinic MapToEntity(VirtualClinicDTO clinicDTO)
		=> new()
		{
			Id = clinicDTO.Id,
			DoctorId = clinicDTO.DoctorId,
			Location = clinicDTO.Location,
			Status = clinicDTO.Status,
			PreviewCost = clinicDTO.PreviewCost,
			AvgService = clinicDTO.AvgService,
			Doctor = new()
		};

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
		return Task.FromResult((new List<ClinicWorkTime>() { new ClinicWorkTime() { Id = 1, ClinicId = 2, StartWorkHours = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)), EndWorkHours = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14)) } }).AsEnumerable());
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

	public Task<VirtualClinicDTO> GetClinicsByDoctorId(int doctorId)
	{
		throw new NotImplementedException();
	}
}
