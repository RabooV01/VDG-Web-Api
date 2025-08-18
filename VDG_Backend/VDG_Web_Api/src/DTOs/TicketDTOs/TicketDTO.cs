using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.DTOs.TicketDTOs
{

	public class TicketDTO
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int DoctorId { get; set; }
		public string Description { get; set; }
		public TicketStatus Status { get; set; }
		public DateTime OpenDate { get; set; }
		public DateTime? CloseDate { get; set; }
	}
}
