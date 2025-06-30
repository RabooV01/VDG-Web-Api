using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories;

public class ReservationRepository : IReservationRepository
{
	private readonly VdgDbDemoContext _context;
	private readonly IVirtualClinicRepository _virtualClinicRepository;

	public ReservationRepository(VdgDbDemoContext context, IVirtualClinicRepository virtualClinicRepository)
	{
		_context = context;
		_virtualClinicRepository = virtualClinicRepository;
	}

	public async Task<IEnumerable<Reservation>> GetClinicReservationsAsync(int virtualId, DateOnly? date)
	{
		if (date == null)
		{
			throw new ArgumentNullException("Must provide date to get reservations");
		}

		try
		{
			// var reservations = _context.Reservations.AsQueryable();

			// return all reservations that related to a clinic
			var reservations = await _context.Reservations
			.Include(r => r.User)
				.ThenInclude(u => u.Person)
			.Where(c =>
			c.ScheduledAt.Year == date.Value.Year &&
			c.ScheduledAt.Month == date.Value.Month &&
			c.ScheduledAt.Day == date.Value.Day)
			.Where(c => c.VirtualId == virtualId)
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

			//var clinics = reservations.Select(x => _virtualClinicRepository.GetClinicById(x.VirtualId!.Value));

			//var mappedReservations = reservations.Join(clinics, x => x.VirtualId, i => i.Id, (Reservation r, VirtualClinic? vc) => { r.Virtual = vc; return r; });

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
		return await _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
	}
}