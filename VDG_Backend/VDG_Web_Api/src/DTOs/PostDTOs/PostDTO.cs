namespace VDG_Web_Api.src.DTOs.PostDTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Content { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpeciality { get; set; }
        public string? ImageUrl { get; set; }
    }
}
