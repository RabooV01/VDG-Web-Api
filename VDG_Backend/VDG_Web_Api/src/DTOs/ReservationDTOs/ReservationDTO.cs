using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class ReservationDTO
{
	public int Id { get; set; }
	public int? UserId { get; set; }
	public int? VirtualId { get; set; }
	public string Text { get; set; } = string.Empty;
	public DateTime ScheduledAt { get; set; }
	public BookingTypes Type { get; set; }
}