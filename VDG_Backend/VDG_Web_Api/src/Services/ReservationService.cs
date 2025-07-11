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
			throw new ArgumentException("Reservation is invalid");
		}

		Reservation reservation = reservationDto.ToEntity();

		var existUserAppointmentsDoctorIds = (await GetUserReservationsAsync(reservation.UserId))
		.Where(r => r.ScheduledAt > DateTime.Now)
		.Select(r => r.VirtualDto!.DoctorId);
		
		var currentAppointmentDoctorId = (await _virtualClinicService.GetClinicById(reservationDto.VirtualId)).Doctor?.Id;

		bool HasAppointment = existUserAppointmentsDoctorIds.Any(c => c == currentAppointmentDoctorId);

		if (HasAppointment)
		{
			throw new InvalidOperationException("Appointment has not been reserved because there is an exist one with same doctor");
		}

		try
		{
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

		if (reservation.ScheduledAt.Subtract(DateTime.Now).Hours < _confirmThresholdPerHours)
		{
			throw new ArgumentException("Appointment has been confirmed and cannot be canceled");
		}

		try
		{
			await _reservationRepository.CancelAppointmentAsync(reservationId);
		}
		catch (KeyNotFoundException ex)
		{
			throw new InvalidOperationException($"Unable to cancel appointment: {ex.Message}", ex);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Error occured while canceling the appointment: {ex.Message}", ex);
		}
	}

	public async Task<IEnumerable<Reservation>> GenerateClinicAvailableReservations(IEnumerable<Reservation> busyAppointments, int virtualId, DateOnly date)
	{
		var clinic = await _virtualClinicService.GetClinicById(virtualId);
		var workTimes = (await _virtualClinicService.GetClinicWorkTimes(virtualId)).ToArray();

		Dictionary<DateTime, Reservation> reservations = busyAppointments.ToDictionary(x => x.ScheduledAt);

		for (int i = 0; i < workTimes.Count(); i++)
		{
			DateTime lastTiming = DateTime.Parse($"{date} {workTimes[i].StartWorkHours}");
			DateTime endTiming = DateTime.Parse($"{date} {workTimes[i].EndWorkHours}");

			while (lastTiming < endTiming)
			{
				if (!reservations.ContainsKey(lastTiming))
				{
					reservations.Add(lastTiming, new() { ScheduledAt = lastTiming });
				}

				lastTiming = lastTiming.AddMinutes(clinic.AvgService);
			}
		}
		return reservations.Select(d => d.Value);
	}

	public async Task<IEnumerable<ClinicReservationDTO>> GetClinicReservationsAsync(int virtualClinicId, DateTime date)
	{
		try
		{
			// get all reservations for a certain date
			var reservations = await _reservationRepository.GetClinicReservationsAsync(virtualClinicId, date);
//
			
			var allReservations = await GenerateClinicAvailableReservations(reservations, virtualClinicId, DateOnly.FromDateTime(date));
			return allReservations.Select(r => r.ToClinicReservationDto())
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

	public async Task EditAppointmentAsync(ReservationDTO reservationDto)
	{
		if (!reservationDto.IsValidReservation())
		{
			throw new ArgumentException("Reservation value is invalid, No update were applied.");
		}

		var clinicReservations = await GetClinicReservationsAsync(reservationDto.VirtualId, reservationDto.ScheduledAt);
		bool isValid = !clinicReservations.Any(r => r.ScheduledAt == reservationDto.ScheduledAt && r.Id != reservationDto.Id);

		if(!isValid)
		{
			throw new ArgumentException("Appointment already has been taken, select another time in order to change");
		}

		Reservation reservation = reservationDto.ToEntity();

		try
		{
			await _reservationRepository.UpdateAppointmentAsync(reservation);
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