namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class ReservationDTO
{
    public int? UserId { get; set; }
    public int? VirtualId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public string? Type { get; set; }
}