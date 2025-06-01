using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class ClinicReservation 
{
    public int Id { get; set; }
	public int? UserId { get; set; }
	public int? VritualId { get; set; }
	public DateTime ScheduledAt { get; set; }
	public string? Text { get; set; } 
    public string? Type { get; set; }
    public User? User { get; set; }
}