namespace VDG_Web_Api.src.DTOs.PostDTOs
{
    public class UpdatePostDTO
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
    }
}
