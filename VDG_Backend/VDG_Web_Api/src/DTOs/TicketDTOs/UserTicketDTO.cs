using VDG_Web_Api.src.DTOs.DoctorDTOs;

namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
    public class UserTicketDTO
    {
        public DoctorDTO DoctorDto { get; set; } = null!;
        public TicketDTO TicketDto { get; set; } = null!;

    }
}
