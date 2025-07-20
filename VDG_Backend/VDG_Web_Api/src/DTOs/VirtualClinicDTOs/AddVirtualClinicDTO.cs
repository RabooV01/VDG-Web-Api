namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class AddVirtualClinicDTO
{
    public int DoctorId { get; set; }
    public string? Name { get; set; }
	public int AvgService { get; set; }
	public double PreviewCost { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? LocationCoords { get; set; }
    public List<DayOfWeek> Holidays = new();
    public ICollection<ClinicWorkTimeDTO> WorkTimes { get; set; } = new List<ClinicWorkTimeDTO>();
}