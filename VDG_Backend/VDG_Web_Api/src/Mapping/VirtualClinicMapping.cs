using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping;

public static class VirtualClinicMapping
{
	public static VirtualClinicInProfileDTO ToClinicInProfileDto(this VirtualClinic clinic)
		=> new()
		{
			Id = clinic.Id,
			DoctorId = clinic.DoctorId,
			EndWorkHours = clinic.WorkTimes.OrderByDescending(w => w.EndWorkHours).FirstOrDefault()?.EndWorkHours ?? new TimeOnly(0),
			StartWorkHours = clinic.WorkTimes.OrderBy(w => w.StartWorkHours).FirstOrDefault()?.StartWorkHours ?? new TimeOnly(0),
			Location = clinic.Location,
			PreviewCost = clinic.PreviewCost,
			Status = clinic.Status,
			Name = clinic.Name
		};

	public static UserReservationDTO ToClinicHighlightDto(this Reservation reservation)
		=> new()
		{
			Id = reservation.Id,
			VirtualClinic = reservation.Virtual.ToClinicInfo(),
			ScheduledAt = reservation.ScheduledAt,
			Text = reservation.Text,
			UserId = reservation.UserId,
			Type = reservation.Type,
			VirtualId = reservation.VirtualId
		};
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
			EndWorkHours = clinicWorkTimeDTO.EndWorkHours,
			DayOfWeek = Enum.Parse<DayOfWeek>(clinicWorkTimeDTO.Day)
		};

	public static ClinicWorkTimeDTO ToDto(this ClinicWorkTime wt) => new()
	{
		Id = wt.Id,
		ClinicId = wt.ClinicId,
		StartWorkHours = wt.StartWorkHours,
		EndWorkHours = wt.EndWorkHours,
		Day = wt.DayOfWeek.ToString()
	};

	public static VirtualClinicInfo ToClinicInfo(this VirtualClinic clinic)
		=> new()
		{
			Id = clinic.Id,
			Doctor = clinic.Doctor.ToInfo(),
			Location = clinic.Location,
			Name = clinic.Name ?? string.Empty
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
			Name = clinicDTO.Name,
			Status = "active"
		};

	public static VirtualClinic ToEntity(this UpdateVirtualClinicDTO updateVirtualClinicDTO)
		=> new()
		{
			Id = updateVirtualClinicDTO.Id,
			AvgService = updateVirtualClinicDTO.AvgService,
			DoctorId = updateVirtualClinicDTO.DoctorId,
			Location = updateVirtualClinicDTO.Location,
			PreviewCost = updateVirtualClinicDTO.PreviewCost,
			Status = updateVirtualClinicDTO.Status,
			Name = updateVirtualClinicDTO.Name,
			LocationCoords = updateVirtualClinicDTO.LocationCoords
		};
}