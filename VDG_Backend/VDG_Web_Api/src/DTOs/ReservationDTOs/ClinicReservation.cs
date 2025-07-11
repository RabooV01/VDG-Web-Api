using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class ClinicReservationDTO : ReservationDTO
{
    public UserDTO UserDto { get; set; } = null!;
}