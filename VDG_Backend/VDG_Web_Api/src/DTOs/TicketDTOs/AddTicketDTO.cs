namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
	public class AddTicketDTO
	{
		public int UserId { get; set; }
		public int DoctorId { get; set; }
		public string Text { get; set; } = string.Empty;
		public TicketMessageDTO TicketMessage { get; set; } = null!;
	}
}
