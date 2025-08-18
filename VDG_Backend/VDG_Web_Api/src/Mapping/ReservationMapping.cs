using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping;

public static class ReservationMapping
{
	public static Reservation ToEntity(this ReservationDTO reservationDTO)
		=> new()
		{
			Id = reservationDTO.Id,
			Text = reservationDTO.Text,
			ScheduledAt = reservationDTO.ScheduledAt,
			Type = reservationDTO.Type,
			UserId = reservationDTO.UserId,
			VirtualId = reservationDTO.VirtualId,
			Status = reservationDTO.Status
		};

	public static ReservationDTO ToDto(this Reservation reservation)
		=> new()
		{
			Id = reservation.Id,
			ScheduledAt = reservation.ScheduledAt,
			Text = reservation.Text,
			Type = reservation.Type,
			UserId = reservation.UserId,
			VirtualId = reservation.VirtualId,
			Status = reservation.Status
		};

	public static ClinicReservationDTO ToClinicReservationDto(this Reservation reservation)
		=> new()
		{
			Id = reservation.Id,
			ScheduledAt = reservation.ScheduledAt,
			Text = reservation.Text,
			Type = reservation.Type,
			UserId = reservation.UserId,
			VirtualId = reservation.VirtualId,
			User = reservation.User?.ToDto(),
			Status = reservation.Status
		};

	public static UserReservationDTO ToUserReservationDto(this Reservation reservation)
	   => new()
	   {
		   Id = reservation.Id,
		   ScheduledAt = reservation.ScheduledAt,
		   Text = reservation.Text,
		   Type = reservation.Type,
		   UserId = reservation.UserId,
		   VirtualId = reservation.VirtualId,
		   VirtualClinic = reservation.Virtual.ToClinicInfo(),
		   Status = reservation.Status
	   };
}