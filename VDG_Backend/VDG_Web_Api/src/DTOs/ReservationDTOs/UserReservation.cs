using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class UserReservationDTO
{
    public ReservationDTO? ReservationDto { get; set; }
    public VirtualClinicDTO? VirtualDto { get; set; }
}