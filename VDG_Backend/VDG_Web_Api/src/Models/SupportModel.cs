namespace VDG_Web_Api.src.Models
{
	public class SupportModel
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public bool Reviewed { get; set; } = false;
		public DateTime SentAt { get; set; } = DateTime.Now;
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
