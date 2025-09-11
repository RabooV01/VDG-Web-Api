namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
	public class StatisticTicketDTO
	{
		public string Date { get; set; }
		public int OpenTickets { get; set; }

		public int ClosedTickets { get; set; }

		public int PendingTickets { get; set; }
	}
}
