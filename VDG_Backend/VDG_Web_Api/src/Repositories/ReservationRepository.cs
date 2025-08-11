using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories;

public class ReservationRepository : IReservationRepository
{
	private readonly VdgDbDemoContext _context;

	public ReservationRepository(VdgDbDemoContext context, IVirtualClinicRepository virtualClinicRepository)
	{
		_context = context;
	}

	public async Task<IEnumerable<Reservation>> GetClinicReservationsAsync(int virtualId, DateTime date)
	{
		try
		{
			var reservations = await _context.Reservations
			.Include(r => r.User)
				.ThenInclude(u => u.Person)
			.Where(c => c.VirtualId == virtualId && c.ScheduledAt.Date == date.Date)
			.ToListAsync();

			return reservations;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Error while retrieving data. {ex.Message}", ex);
		}
	}

	public async Task<IEnumerable<Reservation>> GetUserReservationsAsync(int userId)
	{
		try
		{
			var reservations = await _context.Reservations
			.Include(r => r.Virtual)
				.ThenInclude(v => v.Doctor)
				.ThenInclude(d => d.User)
				.ThenInclude(u => u.Person)
			.Include(r => r.Virtual)
				.ThenInclude(v => v.Doctor)
				.ThenInclude(d => d.Speciality)
			.Where(c => c.UserId == userId)
			.ToListAsync();

			return reservations;

		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Error while retrieving data. {ex.Message}", ex);
		}
	}

	public async Task BookAppointmentAsync(Reservation reservation)
	{
		try
		{
			_context.Reservations.Add(reservation);
			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Failed to add appointment, Error: {e.Message}", e);
		}
	}

	public async Task CancelAppointmentAsync(int reservationId)
	{
		var reservation = await _context.Reservations.FindAsync(reservationId);

		if (reservation == null)
		{
			throw new KeyNotFoundException("No such appointment found; it may have been canceled already.");
		}

		try
		{
			_context.Reservations.Remove(reservation);
			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"Failed to cancel appointment, Error: {e.Message}", e);
		}
	}

	public async Task UpdateAppointmentAsync(Reservation reservation)
	{
		var existingReservation = await _context.Reservations.FindAsync(reservation.Id);

		if (existingReservation == null)
		{
			throw new InvalidOperationException("Appointment has not been found.");
		}

		existingReservation.ScheduledAt = reservation.ScheduledAt;
		existingReservation.Text = reservation.Text;

		try
		{
			_context.Update(existingReservation);
			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"An error occurred while updating the appointment. {e.Message}", e);
		}
	}

	public async Task<Reservation?> GetReservationByIdAsync(int reservationId)
	{
		return await _context.Reservations
			.Include(r => r.Virtual)
				.ThenInclude(v => v.Doctor)
				.ThenInclude(d => d.User)
				.ThenInclude(u => u.Person)
			.Include(r => r.User)
				.ThenInclude(u => u.Person)
			.FirstOrDefaultAsync(r => r.Id == reservationId);
	}

	public async Task PreviewReservation(int reservationId)
	{
		try
		{
			await _context.Reservations.Where(r => r.Id == reservationId)
				.ExecuteUpdateAsync(r => r.SetProperty(p => p.Status, ReservationStatus.Previewed));
		}
		catch (Exception e)
		{
			throw new Exception("Could not set reservation to previewed.", e);
		}
	}

	public async Task<IEnumerable<Reservation>> GetClinicReservationInMonth(int clinicId, DateTime date)
	{
		try
		{
			var fromDate = date.AddDays(-1 * date.Day + 1);
			var ToDate = fromDate.AddMonths(1);

			var reservations = await _context.Reservations
				.Where(r => r.VirtualId == clinicId && r.ScheduledAt.Date >= fromDate && r.ScheduledAt < ToDate)
				.ToListAsync();

			return reservations;
		}
		catch (Exception e)
		{
			throw new Exception("Error occured while retrieving data.", e);
		}
	}

	//public async Task<int> GetClinicReservationCapacity(int clinicId)
	//{
	//	try
	//	{
	//		var clinic = await _context.VirtualClinics.Include(c => c.WorkTimes)
	//			.Where(c => c.Id == clinicId)
	//			.FirstOrDefaultAsync();

	//		if (clinic == null)
	//		{
	//			throw new ArgumentException("Invalid clinic.", nameof(clinic));
	//		}

	//		var capacity = clinic.WorkTimes.
	//	}
	//	catch (Exception)
	//	{

	//		throw;
	//	}
	//}
}