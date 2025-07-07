using VDG_Web_Api.src.DTOs.DoctorDTOs;

namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class VirtualClinicDTO
{
	public int Id { get; set; }

	public int DoctorId { get; set; }

	public DoctorDTO Doctor { get; set; } = null!;

	public TimeOnly StartWorkHours { get; set; }

	public TimeOnly EndWorkHours { get; set; }

	public string Status { get; set; } = "Inactive";

	public int AvgService { get; set; }

	public string Location { get; set; } = string.Empty;

	public double PreviewCost { get; set; }
}