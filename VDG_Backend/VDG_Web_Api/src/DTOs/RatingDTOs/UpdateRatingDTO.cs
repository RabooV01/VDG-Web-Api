namespace VDG_Web_Api.src.DTOs.RatingDTOs
{
    public class UpdateRatingDTO
    {
        public int Id { get; set; }
        public double AvgWait { get; set; }
        public double AvgService { get; set; }
        public double Act { get; set; }
    }
}
