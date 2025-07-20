using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Extensions.Validation;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Services.Interfaces;

public class ReservationService : IReservationService
{
	private readonly IReservationRepository _reservationRepository;
	private readonly IVirtualClinicService _virtualClinicService;
	private readonly IUserService _userService;

	private readonly static int _confirmThresholdPerHours = 24;

	public ReservationService(IReservationRepository reservationRepository,
		IVirtualClinicService virtualClinicService,
		IUserService userService)
	{
		_reservationRepository = reservationRepository;
		_virtualClinicService = virtualClinicService;
		_userService = userService;
	}



	public async Task BookAppointmentAsync(ReservationDTO reservationDto)
	{
		if (!reservationDto.IsValidReservation())
		{
			throw new ArgumentException("appointment time is invalid");
		}

		try
		{
			var currentClinic = await _virtualClinicService.GetClinicById(reservationDto.VirtualId);

			bool isHoliday = currentClinic.Holidays.Any(h => h.Equals(reservationDto.ScheduledAt.DayOfWeek));

			if (isHoliday)
			{
				throw new ArgumentException("appointments cannot be on a holiday");
			}

			Reservation reservation = reservationDto.ToEntity();

			var existUserAppointmentsDoctorIds = (await GetUserReservationsAsync(reservation.UserId))
			.Where(r => r.ScheduledAt > DateTime.Now)
			.Select(r => r.VirtualClinic!.DoctorId);

			if (await HasConflict(reservationDto))
			{
				throw new ArgumentException("reservation has conflict");
			}

			var currentAppointmentDoctorId = currentClinic.Doctor?.DoctorId;

			bool HasAppointment = existUserAppointmentsDoctorIds.Any(c => c == currentAppointmentDoctorId);

			if (HasAppointment)
			{
				throw new InvalidOperationException("Appointment has not been reserved because there is an exist one with same doctor");
			}

			await _reservationRepository.BookAppointmentAsync(reservation);
		}
		catch (InvalidOperationException ex)
		{
			throw new InvalidOperationException($"Error occured while booking the appointment: {ex.Message}", ex);
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Unexpected error occurred: {e.Message}", e);
		}
	}

	public async Task CancelAppointmentAsync(int reservationId)
	{
		var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);

		if (reservation == null)
		{
			throw new KeyNotFoundException("No such reservation is found.");
		}

		var differenceInHours = reservation.ScheduledAt.Subtract(DateTime.Now);

		if (differenceInHours < TimeSpan.FromHours(_confirmThresholdPerHours))
		{
			throw new ArgumentException("Appointment has been confirmed and cannot be canceled");
		}

		try
		{
			await _reservationRepository.CancelAppointmentAsync(reservationId);
		}
		catch (KeyNotFoundException)
		{
			throw;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Error occured while canceling the appointment: {ex.Message}", ex);
		}
	}

	public async Task<Dictionary<DateTime, Reservation>> GenerateClinicAvailableReservations(int virtualId, DateTime date)
	{
		// get all reservations for a certain date
		Dictionary<DateTime, Reservation> reservations = (await _reservationRepository.GetClinicReservationsAsync(virtualId, date)).ToDictionary(x => x.ScheduledAt);
		var clinic = await _virtualClinicService.GetClinicById(virtualId);
		var workTimes = clinic.WorkTimes.ToArray();

		for (int i = 0; i < workTimes.Length; i++)
		{
			DateTime lastTiming = DateTime.Parse($"{date.Date.Add(workTimes[i].StartWorkHours.ToTimeSpan())}");
			DateTime endTiming = DateTime.Parse($"{date.Add(workTimes[i].EndWorkHours.ToTimeSpan())}");

			while (lastTiming < endTiming)
			{
				if (!reservations.ContainsKey(lastTiming))
				{
					reservations.Add(lastTiming, new() { ScheduledAt = lastTiming });
				}

				lastTiming = lastTiming.AddMinutes(clinic.AvgService);
			}
		}
		return reservations;
	}

	public async Task<IEnumerable<ClinicReservationDTO>> GetClinicReservationsAsync(int virtualClinicId, DateTime date)
	{
		try
		{
			var allReservations = await GenerateClinicAvailableReservations(virtualClinicId, date);
			return allReservations.Select(r => r.Value.ToClinicReservationDto())
			.OrderBy(x => x.ScheduledAt)
			.ToList();
		}
		catch (InvalidOperationException ex)
		{
			throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<IEnumerable<UserReservationDTO>> GetUserReservationsAsync(int userId, DateOnly? date = null)
	{
		try
		{
			var reservations = await _reservationRepository.GetUserReservationsAsync(userId);

			return reservations.Select(r => r.ToUserReservationDto())
				.OrderBy(r => r.ScheduledAt);
		}
		catch (InvalidOperationException ex)
		{
			throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
		}
		catch (Exception)
		{
			throw;
		}
	}

	private async Task<bool> HasConflict(ReservationDTO reservationDto)
	{
		var clinicReservations = await GenerateClinicAvailableReservations(reservationDto.VirtualId, reservationDto.ScheduledAt);

		if (!clinicReservations.TryGetValue(reservationDto.ScheduledAt, out var existsReservationTime))
		{
			throw new ArgumentException("Invalid date provided, Selected date is not listed on clinic worktimes");
		}

		if (existsReservationTime.User != null)
		{
			return true;
		}

		return false;
	}

	public async Task<bool> EditAppointmentAsync(ReservationDTO reservationDto)
	{
		if (!reservationDto.IsValidReservation())
		{
			throw new ArgumentException("Reservation value is invalid, No update was applied.");
		}

		try
		{
			var currentClinic = await _virtualClinicService.GetClinicById(reservationDto.VirtualId);

			bool isHoliday = currentClinic.Holidays.Any(h => h.Equals(reservationDto.ScheduledAt.DayOfWeek));

			if (isHoliday)
			{
				throw new ArgumentException("appointments cannot be on a holiday");
			}

			if (await HasConflict(reservationDto))
			{
				return false;
			}

			Reservation reservation = reservationDto.ToEntity();
			await _reservationRepository.UpdateAppointmentAsync(reservation);

			return true;
		}
		catch (ArgumentException)
		{
			throw;
		}
		catch (InvalidOperationException ex)
		{
			throw new InvalidOperationException($"Update failed, Error: {ex.Message}", ex);
		}
		catch (Exception)
		{
			throw;
		}
	}
}