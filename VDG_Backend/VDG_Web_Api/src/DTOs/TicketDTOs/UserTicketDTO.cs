using VDG_Web_Api.src.DTOs.DoctorDTOs;

namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
    public class UserTicketDTO
    {
        DoctorDTO? DoctorDto { get; set; }
        TicketDTO? TicketDto { get; set; }

    }
}
