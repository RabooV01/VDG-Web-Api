namespace VDG_Web_Api.src.DTOs.PromotionRequest
{
	public class PromotionRequestDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string FullName { get; set; }
		public string SyndicateId { get; set; }
		public string Personal_Id { get; set; }
		public string Note { get; set; }
		public string? ApprovedBy { get; set; } = null;
		public int? AdminId { get; set; } = null;
		public DateTime? ApprovementDate { get; set; } = null;
	}
}
