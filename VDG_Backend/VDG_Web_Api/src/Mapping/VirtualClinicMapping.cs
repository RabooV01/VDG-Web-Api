using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping;

public static class VirtualClinicMapping
{
    public static VirtualClinic ToEntity(this VirtualClinicDTO virtualClinicDTO)
        => new()
        {
            Id = virtualClinicDTO.Id,
            DoctorId = virtualClinicDTO.DoctorId,
            Location = virtualClinicDTO.Location,
            AvgService = virtualClinicDTO.AvgService,
            PreviewCost = virtualClinicDTO.PreviewCost,
            Status = virtualClinicDTO.Status,
            Doctor = virtualClinicDTO.Doctor.ToDto()
        };

    public static ClinicWorkTime ToEntity(this ClinicWorkTimeDTO clinicWorkTimeDTO)
        => new()
        {
            Id = clinicWorkTimeDTO.Id,
            ClinicId = clinicWorkTimeDTO.ClinicId,
            StartWorkHours = clinicWorkTimeDTO.StartWorkHours,
            EndWorkHours = clinicWorkTimeDTO.EndWorkHours
        };

    public static VirtualClinicDTO ToClinicDto(this VirtualClinic virtualClinic)
        => new()
        {
            Id = virtualClinic.Id,
            DoctorId = virtualClinic.DoctorId,
            Location = virtualClinic.Location,
            AvgService = virtualClinic.AvgService,
            Status = virtualClinic.Status,
            StartWorkHours = virtualClinic.WorkTimes.OrderBy(x => x.StartWorkHours).First().StartWorkHours,
            EndWorkHours = virtualClinic.WorkTimes.OrderByDescending(x => x.EndWorkHours).First().EndWorkHours
        };

    public static ClinicWorkTimeDTO MapToDTO(this ClinicWorkTime wt) => new() 
	{
		Id = wt.Id,
		ClinicId = wt.ClinicId,
		StartWorkHours = wt.StartWorkHours,
		EndWorkHours = wt.EndWorkHours
	};

	public static VirtualClinicDTO MapToDTO(this VirtualClinic clinic)
		=> new()
		{
			Id = clinic.Id,
			DoctorId = clinic.DoctorId,
			Status = clinic.Status,
			Location = clinic.Location,
			AvgService = clinic.AvgService,
			Doctor = clinic.Doctor.MapToDto(),
			PreviewCost = clinic.PreviewCost,
            StartWorkHours = clinic.WorkTimes.OrderBy(x => x.StartWorkHours).First().StartWorkHours,
            EndWorkHours = clinic.WorkTimes.OrderByDescending( x => x.EndWorkHours).First().EndWorkHours
		};

	public  static VirtualClinic MapToEntity(this AddVirtualClinicDTO clinicDTO)
		=> new()
		{
			DoctorId = clinicDTO.DoctorId,
			Location = clinicDTO.Location,
			PreviewCost = clinicDTO.PreviewCost,
			AvgService = clinicDTO.AvgService,
            WorkTimes = clinicDTO.WorkTimes.Select(x => x.ToEntity()).ToList()
		};
}