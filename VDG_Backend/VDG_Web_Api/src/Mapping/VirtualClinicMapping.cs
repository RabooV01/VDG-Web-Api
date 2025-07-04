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
            Status = virtualClinicDTO.Status 
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
        }
}