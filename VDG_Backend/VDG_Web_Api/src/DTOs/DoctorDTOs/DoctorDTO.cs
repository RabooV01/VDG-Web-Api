using VDG_Web_Api.src.DTOs.UserDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.DoctorDTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string SyndicateId { get; set; } = string.Empty;

        public int? UserId { get; set; }

        public virtual UserDTO? User { get; set; }

        public int? SpecialityId { get; set; }

        public virtual Speciality? Speciality { get; set; }

    }
}
