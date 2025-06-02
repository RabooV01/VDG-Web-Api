using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.DTOs.ReservationDTOs;

public class UserReservation 
{
    ReservationDTO? Reservation { get; set; }
    public VirtualClinic? Clinic { get; set; }
}