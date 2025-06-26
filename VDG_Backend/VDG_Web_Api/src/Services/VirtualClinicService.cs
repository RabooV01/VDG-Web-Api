using System.Globalization;
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

    public async Task AddClinic(VirtualClinicDTO clinic)
    {
        if(clinic.StartWorkHours == null || clinic.EndWorkHours == null)
		{
			throw new ArgumentNullException("Must provide initial workTime range");
		}
		
		try
		{
			ClinicWorkTime workTime = new() { StartWorkHours = clinic.StartWorkHours, EndWorkHours = clinic.EndWorkHours };

			VirtualClinic virtualClinic = new () {
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

    public Task AddClinicWorkTime(ClinicWorkTimeDTO workTimeDTO)
    {
        throw new NotImplementedException();
    }

    public Task DeleteClinic(int clinicId)
    {
        throw new NotImplementedException();
    }

    public Task<VirtualClinicDTO> GetClinicById(int clinicId)
	{
		throw new NotImplementedException();
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
