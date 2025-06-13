using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
    public class DoctorTicketDTO
    {
        TicketDTO? TicketDto { get; set; }
        UserDTO? UserDto { get; set; }
    }
}
