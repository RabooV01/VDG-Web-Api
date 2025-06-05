using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
    public class DoctorTicketDTO
    {
        UserDTO? UserDto { get; set; }
        TicketDTO? TicketDto { get; set; }
    }
}
