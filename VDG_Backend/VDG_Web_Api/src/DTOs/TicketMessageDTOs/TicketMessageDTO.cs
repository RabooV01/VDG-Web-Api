namespace VDG_Web_Api.src.DTOs.MessageDTOs
{
    public class TicketMessageDTO
    {
        public int Id { get; set; }

        public int? TicketId { get; set; }
        public int? OwnerId { get; set; }
        public string? Txt { get; set; }
        public DateTime? Date { get; set; }

    }
}
