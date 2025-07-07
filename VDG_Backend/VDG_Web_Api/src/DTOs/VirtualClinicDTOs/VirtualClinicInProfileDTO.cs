namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class VirtualClinicInProfileDTO
{
	public int Id { get; set; }

	public int? DoctorId { get; set; }

	public string Status { get; set; } = "Inactive";

	public string? Location { get; set; }

	public double? PreviewCost { get; set; }

	public TimeOnly StartWorkHours { get; set; }

	public TimeOnly EndWorkHours { get; set; }
}