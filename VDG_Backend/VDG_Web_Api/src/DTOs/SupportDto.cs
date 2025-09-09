using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.DTOs
{
	public class SupportDto
	{
		public int Id { get; set; }
		public string UserFullName { get; set; }
		public UserRole Role { get; set; }
		public DateTime SentAt { get; set; }
		public int UserId { get; set; }
		public bool IsReviewed { get; set; }
	}
}
