using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.Models
{
	public class PromotionRequest
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public int SpecialityId { get; set; }
		public Speciality Speciality { get; set; }
		public DateTime RequestedAt { get; set; }
		public string SyndicateId { get; set; }
		public string Note { get; set; }
		public PromotionStatus Status { get; set; } = PromotionStatus.Pending;
		public DateTime? ResponseDate { get; set; } = null;
		public int? RespondBy { get; set; } = null;
		public User Admin { get; set; }
	}
}
