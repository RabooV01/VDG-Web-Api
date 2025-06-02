using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories;

public class ReservationRepository : IReservationRepository
{
	private readonly VdgDbDemoContext _context;

	public ReservationRepository(VdgDbDemoContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Reservation>> GetReservationsAsync(int? virtualId = null, int? userId = null, DateOnly? day = null)
	{
		var reservations = _context.Reservations
		.Where( c => (c.VritualId == virtualId || virtualId == null) 
		&& (c.UserId == userId || userId == null) 
		&& ( day == null || DateOnly.Parse(c.ScheduledAt.ToString()) == day));

		return await reservations.ToListAsync();
	}

	public async Task BookAppointmentAsync(Reservation reservation)
	{
		if(reservation.UserId == null || reservation.VritualId == null)
			throw new ArgumentNullException(nameof(reservation), "must selected a user and a clinic");
		try
		{
			_context.Reservations.Add(reservation);
			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"failed to add appointment, Error: {e.Message}", e);
		}
	}

	public async Task CancelAppointmentAsync(int reservationId)
	{
		try
		{
			var reservation = await _context.Reservations.FindAsync(reservationId);
		
			if(reservation == null)
			{
				throw new Exception("No such appointment found; it may have been canceled already");
			}

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
		try
		{
			if (reservation == null)
			{
				throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null.");
			}
			
			var existingReservation = await _context.Reservations.FindAsync(reservation.Id);
			
			if (existingReservation == null)
			{
				throw new InvalidOperationException("Appointment has not been found.");
			}
			
			existingReservation.ScheduledAt = reservation.ScheduledAt;
			existingReservation.Text = reservation.Text;
			existingReservation.Type = reservation.Type;

			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"An error occurred while updating the appointment. {e.Message}", e);
		}
	}
}