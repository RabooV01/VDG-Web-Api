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

	public async Task<IEnumerable<Reservation>> GetReservationsAsync(int? virtualId = null, int? userId = null, DateOnly? date = null)
	{
		if(virtualId != null && userId != null)
		{
			throw new ArgumentException("Must select atmost a user or a clinic");
		}
		
		try
		{
			var reservations = _context.Reservations.AsQueryable();

			if(date != null)
			{
				reservations = _context.Reservations.Where(c => 
				c.ScheduledAt.Year == date.Value.Year && 
				c.ScheduledAt.Month == date.Value.Month && 
				c.ScheduledAt.Day == date.Value.Day);
			}

			if(virtualId != null)
			{
				// return all reservations that related to a clinic
				return await reservations
				.Where(c => c.VirtualId == virtualId)
				.ToListAsync();
			}
			if(userId != null)
			{
				// return all reservations that related to a user
				return await reservations
				.Where(c => c.UserId == userId)
				.ToListAsync();
			}
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Error while retrieving data. {ex.Message}", ex);
		}

		throw new InvalidOperationException($"Unexpected error occured in {nameof(GetReservationsAsync)} method controlflow.");
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
		
		if(reservation == null)
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
		existingReservation.Type = reservation.Type;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			throw new InvalidOperationException($"An error occurred while updating the appointment. {e.Message}", e);
		}
	}
}