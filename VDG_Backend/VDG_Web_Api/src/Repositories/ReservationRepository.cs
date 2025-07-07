using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories;

public class ReservationRepository : IDoctorRepository
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
			.Where(c => c.ScheduledAt == date && c.VirtualId == virtualId)
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
}