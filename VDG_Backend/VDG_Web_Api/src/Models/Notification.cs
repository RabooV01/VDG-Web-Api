namespace VDG_Web_Api.src.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Message { get; set; }

        public int? AdminId { get; set; }

        public User Admin { get; set; }

        public DateTime Date { get; set; }
    }
}
