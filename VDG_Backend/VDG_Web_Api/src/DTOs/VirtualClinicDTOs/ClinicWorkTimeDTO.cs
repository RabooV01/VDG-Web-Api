using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class ClinicWorkTimeDTO
{
	public int Id { get; set; }

    public int clinicId { get; set; }

	public TimeOnly? StartWorkHours { get; set; }

	public TimeOnly? EndWorkHours { get; set; }
} 