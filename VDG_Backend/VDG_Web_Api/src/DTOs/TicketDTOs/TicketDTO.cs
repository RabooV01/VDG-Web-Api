namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
	public class TicketDTO
	{
		public int Id { get; set; }
		public int? UserId { get; set; }
		public int? DoctorId { get; set; }
		public string? Status { get; set; }
		public DateOnly? CloseDate { get; set; }
	}
}
