using VDG_Web_Api.src.DTOs.DoctorDTOs;

namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class VirtualClinicDTO
{
	public int Id { get; set; }

	public int? DoctorId { get; set; }

	public virtual DoctorDTO? Doctor { get; set; }

	public TimeOnly StartWorkHours { get; set; }

	public TimeOnly EndWorkHours { get; set; }

	public string Status { get; set; } = "Inactive";

	public int AvgService { get; set; }

	public string? Location { get; set; }

	public double? PreviewCost { get; set; }
}