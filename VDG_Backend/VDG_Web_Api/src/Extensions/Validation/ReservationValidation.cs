using VDG_Web_Api.src.DTOs.ReservationDTOs;

namespace VDG_Web_Api.src.Extensions.Validation;


public static class ReservationValidation
{
	public static bool IsValidReservation(this ReservationDTO reservation)
	{
		if (reservation.ScheduledAt < DateTime.Now)
		{
			return false;
		}

		return true;
	}
}