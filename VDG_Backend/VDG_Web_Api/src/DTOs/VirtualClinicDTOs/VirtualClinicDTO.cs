using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class VirtualClinicDTO
{
	public int Id { get; set; }

	public int? DoctorId { get; set; }

	public virtual Doctor? Doctor { get; set; }

	public TimeOnly? StartWorkHours { get; set; }

	public TimeOnly? EndWorkHours { get; set; }

	public string? Status { get; set; }
	
	public double? AvgService { get; set; }

    public string? Location { get; set; }

	public double? PreviewCost { get; set; }
} 