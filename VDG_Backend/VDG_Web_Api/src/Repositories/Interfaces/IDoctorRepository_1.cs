using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface IDoctorRepository
	{
		Task<IEnumerable<Reservation>> GetClinicReservationsAsync(int virtualId, DateOnly? day = null);
		Task<IEnumerable<Reservation>> GetUserReservationsAsync(int userId);
		public Task BookAppointmentAsync(Reservation reservation);
		public Task CancelAppointmentAsync(int reservationId);
		public Task UpdateAppointmentAsync(Reservation reservation);
		Task<Reservation?> GetReservationByIdAsync(int reservationId);
	}
}
