using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Extensions.Validation;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Services.Interfaces;

public class ReservationService : IReservationService
{
	private readonly IReservationRepository _reservationRepository;
	private readonly IVirtualClinicService _virtualClinicService;
	private readonly IClaimService _claimService;

	private readonly static int _confirmThresholdPerHours = 24;

	public ReservationService(IReservationRepository reservationRepository,
		IVirtualClinicService virtualClinicService,
		IClaimService claimService)
	{
		_reservationRepository = reservationRepository;
		_virtualClinicService = virtualClinicService;
		_claimService = claimService;
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

			if (!currentClinic.WorkTimes.Any(wt => wt.Day == reservationDto.ScheduledAt.DayOfWeek.ToString()))
			{
				throw new OperationCanceledException("Appointments cannot be on holidays.");
			}

			Reservation reservation = reservationDto.ToEntity();

			var existUserAppointmentsDoctorIds = (await GetUserReservationsAsync(reservation.UserId))
			.Where(r => r.ScheduledAt > DateTime.Now)
			.Select(r => r.VirtualClinic!.Doctor.Id);

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
		catch (Exception)
		{
			throw;
		}
	}
	public IEnumerable<TimeOnly> ExtractSlotsFromWorkTimes(IEnumerable<ClinicWorkTimeDTO> workTimes, int avgService)
	{
		List<TimeOnly> times = new();
		foreach (var workTime in workTimes)
		{
			TimeOnly lastTiming = workTime.StartWorkHours;

			TimeOnly endTiming = workTime.EndWorkHours;
			while (lastTiming < endTiming)
			{
				times.Add(lastTiming);
				lastTiming = lastTiming.AddMinutes(avgService);
			}
		}
		return times;
	}

	public async Task<Dictionary<DateTime, Reservation>> GenerateClinicAvailableReservations(int virtualId, DateTime date)
	{
		try
		{
			// get all reservations for a certain date
			Dictionary<DateTime, Reservation> reservations = (await _reservationRepository.GetClinicReservationsAsync(virtualId, date)).ToDictionary(x => x.ScheduledAt);
			var clinic = await _virtualClinicService.GetClinicById(virtualId);
			var workTimes = clinic.WorkTimes.ToArray();
			var extractedTimes = ExtractSlotsFromWorkTimes(workTimes.Where(wt => wt.Day == date.DayOfWeek.ToString()), clinic.AvgService);

			foreach (var time in extractedTimes)
			{
				var dateTime = date.Date.Add(time.ToTimeSpan());
				if (dateTime > DateTime.Now && !reservations.ContainsKey(dateTime))
				{
					reservations.Add(dateTime, new() { ScheduledAt = dateTime });
				}
			}
			return reservations;
		}
		catch (Exception)
		{
			throw;
		}
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

		try
		{
			if (!reservationDto.IsValidReservation())
			{
				throw new ArgumentException("Reservation value is invalid, No update was applied.");
			}

			var currentClinic = await _virtualClinicService.GetClinicById(reservationDto.VirtualId);

			//bool isHoliday = currentClinic.Holidays.Any(h => h.Equals(reservationDto.ScheduledAt.DayOfWeek));

			//if (isHoliday)
			//{
			//	throw new ArgumentException("appointments cannot be on a holiday");
			//}

			if (await HasConflict(reservationDto))
			{
				return false;
			}

			Reservation reservation = reservationDto.ToEntity();
			await _reservationRepository.UpdateAppointmentAsync(reservation);

			return true;
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

	public async Task PreviewReservation(int reservationId)
	{
		try
		{
			var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
			if (reservation == null)
			{
				throw new ArgumentException($"Reservation is not valid", nameof(reservationId));
			}
			var clinic = await _virtualClinicService.GetClinicById(reservation.VirtualId);

			if (clinic.Doctor.UserId != _claimService.GetCurrentUserId() && !_claimService.IsAdmin())
			{
				throw new UnauthorizedAccessException();
			}

			await _reservationRepository.PreviewReservation(reservationId);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<IEnumerable<ReservationDayBusyness>> GetMonthBusyness(int clinicId, DateTime date)
	{
		// TODO generate all days, and then assign values.
		try
		{
			var reservations = await _reservationRepository.GetClinicReservationInMonth(clinicId, date);
			var clinic = await _virtualClinicService.GetClinicById(clinicId);
			var totalTimes = ExtractSlotsFromWorkTimes(clinic.WorkTimes, clinic.AvgService).Count();

			var ReservationBusyness = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month))
				.Select(day => new DateTime(date.Year, date.Month, day))
				.GroupJoin(reservations, day => day,
				reservation => reservation.ScheduledAt.Date,
				(date, reservations) => new ReservationDayBusyness()
				{
					Day = DateOnly.FromDateTime(date),
					BusynessPercent = $"{(double)reservations.Count() * 100 / (totalTimes == 0 ? 1 : totalTimes):F2}"
				});

			return ReservationBusyness;
		}
		catch (Exception)
		{
			throw;
		}
	}
}