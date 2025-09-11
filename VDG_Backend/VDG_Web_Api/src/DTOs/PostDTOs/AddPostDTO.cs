namespace VDG_Web_Api.src.DTOs.PostDTOs
{
	public class AddPostDTO
	{
		public int DoctorId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string? ImageUrl { get; set; }
	}
}
