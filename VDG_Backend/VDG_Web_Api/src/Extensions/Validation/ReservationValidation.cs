using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Extensions.Validation;


public static class ReservationValidation
{
    public static bool IsValidReservation(this ReservationDTO reservation)
	{
		if (reservation == null)
		{
			return false;
		}

		if (!Enum.TryParse<BookingTypes>(reservation.Type.ToString(), true, out _))
		{
			return false;
		}

		if (reservation.VirtualId == null)
		{
			return false;
		}

		if (reservation.ScheduledAt < DateTime.UtcNow)
		{
			return false;
		}

		return true;
	}
}