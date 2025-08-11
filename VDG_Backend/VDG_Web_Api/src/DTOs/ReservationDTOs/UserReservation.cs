using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class UserReservationDTO : ReservationDTO
{
	public VirtualClinicInfo VirtualClinic { get; set; } = null!;
}