using VDG_Web_Api.src.DTOs.TicketDTOs;

namespace VDG_Web_Api.src.Extensions.Validation
{
	public static class TicketValidation
	{
		public static bool IsValidTicketMessage(this TicketMessageDTO ticketMessageDTO)
		{
			return string.IsNullOrEmpty(ticketMessageDTO.Text);
		}
	}
}
