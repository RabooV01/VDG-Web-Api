using Microsoft.IdentityModel.Tokens;
using VDG_Web_Api.src.DTOs.TicketDTOs;

namespace VDG_Web_Api.src.Extensions.Validation
{
    public static class TicketValidation
    {
        public static bool IsValidTicket(this TicketDTO ticketDTO)
        {
            return ticketDTO.DoctorId.HasValue && ticketDTO.UserId.HasValue;
        }
        public static bool IsValidTicketMessage(this TicketMessageDTO ticketMessageDTO)
        {
            return !ticketMessageDTO.Text.IsNullOrEmpty() && ticketMessageDTO.OwnerId.HasValue;
        }
    }
}
