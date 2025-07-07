using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface IReservationRepository
	{
		Task<IEnumerable<Reservation>> GetClinicReservationsAsync(int virtualId, DateTime date);
		Task<IEnumerable<Reservation>> GetUserReservationsAsync(int userId);
		public Task BookAppointmentAsync(Reservation reservation);
		public Task CancelAppointmentAsync(int reservationId);
		public Task UpdateAppointmentAsync(Reservation reservation);
		Task<Reservation?> GetReservationByIdAsync(int reservationId);
	}
}
