namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class ClinicWorkTimeDTO
{
	public int Id { get; set; }

	public int ClinicId { get; set; }

	public TimeOnly? StartWorkHours { get; set; }

	public TimeOnly? EndWorkHours { get; set; }
}