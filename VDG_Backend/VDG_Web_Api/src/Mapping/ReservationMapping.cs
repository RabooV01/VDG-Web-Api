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
			VirtualId = reservationDTO.VirtualId
		};

	public static ReservationDTO ToDto(this Reservation reservation)
		=> new()
		{
			Id = reservation.Id,
			ScheduledAt = reservation.ScheduledAt,
			Text = reservation.Text,
			Type = reservation.Type,
			UserId = reservation.UserId,
			VirtualId = reservation.VirtualId
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
			UserDto = reservation.User.ToDto()
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
		   VirtualDto = reservation.Virtual?.ToDto()
	   };
}