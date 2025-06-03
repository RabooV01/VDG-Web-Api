using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class ClinicReservationDTO
{
    public ReservationDTO? ReservationDto { get; set; }
    public UserDTO? UserDto { get; set; }
}