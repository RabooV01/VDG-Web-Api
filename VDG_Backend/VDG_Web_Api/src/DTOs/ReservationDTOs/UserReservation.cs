using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class UserReservationDTO : ReservationDTO
{
	public VirtualClinicDTO? VirtualClinic { get; set; } = null!;
}