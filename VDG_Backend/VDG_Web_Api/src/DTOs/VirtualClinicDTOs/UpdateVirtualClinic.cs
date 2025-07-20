namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class UpdateVirtualClinicDTO
{
    public int Id { get; set; }
	public int AvgService { get; set; }
    public int DoctorId { get; set; }
    public string Status { get; set; } = string.Empty;
	public double PreviewCost { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? LocationCoords { get; set; }
}