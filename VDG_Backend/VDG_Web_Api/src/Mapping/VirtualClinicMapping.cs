using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping;

public static class VirtualClinicMapping
{
	public static string ToHolidaysString(this IEnumerable<DayOfWeek> dayOfWeeks)
	{
		string Holidays = "";
		foreach (var holiday in dayOfWeeks)
		{
			Holidays = $"{Holidays}{holiday};";
		}
		return Holidays;
	}
	public static VirtualClinic ToEntity(this VirtualClinicDTO virtualClinicDTO)
	{

		return new()
		{
			Id = virtualClinicDTO.Id,
			DoctorId = virtualClinicDTO.DoctorId,
			Location = virtualClinicDTO.Location,
			AvgService = virtualClinicDTO.AvgService,
			PreviewCost = virtualClinicDTO.PreviewCost,
			Status = virtualClinicDTO.Status,
			Name = virtualClinicDTO.Name,
			Holidays = virtualClinicDTO.Holidays.ToHolidaysString(),
			LocationCoords = virtualClinicDTO.LocationCoords,
			Doctor = virtualClinicDTO.Doctor.ToEntity()
		};
	}

	public static ClinicWorkTime ToEntity(this ClinicWorkTimeDTO clinicWorkTimeDTO)
		=> new()
		{
			Id = clinicWorkTimeDTO.Id,
			ClinicId = clinicWorkTimeDTO.ClinicId,
			StartWorkHours = clinicWorkTimeDTO.StartWorkHours,
			EndWorkHours = clinicWorkTimeDTO.EndWorkHours
		};

	public static ClinicWorkTimeDTO ToDto(this ClinicWorkTime wt) => new()
	{
		Id = wt.Id,
		ClinicId = wt.ClinicId,
		StartWorkHours = wt.StartWorkHours,
		EndWorkHours = wt.EndWorkHours
	};

	public static VirtualClinicDTO ToDto(this VirtualClinic clinic)
		=> new()
		{
			Id = clinic.Id,
			DoctorId = clinic.DoctorId,
			Status = clinic.Status,
			Location = clinic.Location,
			AvgService = clinic.AvgService,
			Doctor = clinic.Doctor.ToDto(),
			PreviewCost = clinic.PreviewCost,
			Name = clinic.Name,
			Holidays = string.IsNullOrEmpty(clinic.Holidays) ? new() : clinic.Holidays.Split(';')
				.Select(d => Enum.Parse<DayOfWeek>(d))
				.ToList(),
			WorkTimes = clinic.WorkTimes.Select(wt => wt.ToDto()).ToList()
		};

	public static VirtualClinic ToEntity(this AddVirtualClinicDTO clinicDTO)
		=> new()
		{
			DoctorId = clinicDTO.DoctorId,
			Location = clinicDTO.Location,
			PreviewCost = clinicDTO.PreviewCost,
			AvgService = clinicDTO.AvgService,
			LocationCoords = clinicDTO.LocationCoords,
			Holidays = clinicDTO.Holidays.ToHolidaysString(),
			Name = clinicDTO.Name,
			Status = "active",
			WorkTimes = clinicDTO.WorkTimes.Select(x => x.ToEntity()).ToList()
		};

	public static VirtualClinic ToEntity(this UpdateVirtualClinicDTO updateVirtualClinicDTO)
		=> new()
		{
			Id = updateVirtualClinicDTO.Id,
			AvgService = updateVirtualClinicDTO.AvgService,
			DoctorId = updateVirtualClinicDTO.DoctorId,
			Location = updateVirtualClinicDTO.Location,
			PreviewCost = updateVirtualClinicDTO.PreviewCost,
			Status = updateVirtualClinicDTO.Status
		};
}