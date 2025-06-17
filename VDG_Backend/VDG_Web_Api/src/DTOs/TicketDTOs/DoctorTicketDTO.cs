using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
    public class DoctorTicketDTO
    {
        public TicketDTO? TicketDto { get; set; }
        public UserDTO? UserDto { get; set; }
    }
}
