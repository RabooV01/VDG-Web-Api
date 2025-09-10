namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
    public class StatisticTicketDTO
    {
        public string Date { get; set; }
        public int OpenDays { get; set; }

        public int CloseDays { get; set; }

        public int PendingDays { get; set; }
    }
}
