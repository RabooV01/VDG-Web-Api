using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.DoctorDTOs
{
    public class DoctorRating
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public double Rating { get; set; }
    }
}
