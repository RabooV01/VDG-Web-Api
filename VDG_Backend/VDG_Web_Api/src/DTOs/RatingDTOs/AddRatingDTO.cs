namespace VDG_Web_Api.src.DTOs.RatingDTOs
{
    public class AddRatingDTO
    {
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public double AvgWait { get; set; }
        public double AvgService { get; set; }
        public double Act { get; set; }
    }
}
