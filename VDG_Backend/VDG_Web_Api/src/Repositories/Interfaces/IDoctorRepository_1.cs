using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface IDoctorRepository
	{
		Task<IEnumerable<Reservation>> GetReservationsAsync(int? virtualId = null, int? userId = null, DateOnly? day = null);
		public Task BookAppointmentAsync(Reservation reservation);
		public Task CancelAppointmentAsync(int reservationId);
		public Task UpdateAppointmentAsync(Reservation reservation);
	}
}
