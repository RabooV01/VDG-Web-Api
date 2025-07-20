using VDG_Web_Api.src.DTOs.DoctorDTOs;

namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class VirtualClinicDTO
{
	public int Id { get; set; }

	public string? Name { get; set; }

	public int DoctorId { get; set; }

	public DoctorDTO Doctor { get; set; } = null!;

	public string Status { get; set; } = "Inactive";

	public int AvgService { get; set; }

	public string Location { get; set; } = string.Empty;

	public string? LocationCoords { get; set; }

	public double PreviewCost { get; set; }

	public List<DayOfWeek> Holidays { get; set; } = new();

	public List<ClinicWorkTimeDTO> WorkTimes { get; set; } = new();
}