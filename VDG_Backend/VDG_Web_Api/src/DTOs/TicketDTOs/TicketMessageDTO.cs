namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
    public class TicketMessageDTO
    {
        public int Id { get; set; }

        public int? TicketId { get; set; }

        public string? Text { get; set; }

        public int? OwnerId { get; set; }

        public DateTime Date { get; set; }

    }
}
