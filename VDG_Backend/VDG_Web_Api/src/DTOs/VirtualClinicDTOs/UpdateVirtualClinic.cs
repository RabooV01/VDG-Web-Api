namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

public class UpdateVirtualClinicDTO
{
    public int Id { get; set; }
	public int AvgService { get; set; }
	public double? PreviewCost { get; set; }
    public string? Location { get; set; }
}