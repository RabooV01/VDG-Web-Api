using VDG_Web_Api.src.DTOs.SpecialityDTOS;
using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.DTOs.DoctorDTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string SyndicateId { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public virtual UserDTO? User { get; set; }
        public int? SpecialityId { get; set; }
        public virtual SpecialityDTO? Speciality { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
