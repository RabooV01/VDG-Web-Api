using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface IReservationRepository
	{
		public Task<IEnumerable<Reservation>> GetReservationStatistics(int virtualId);
		public Task<IEnumerable<Reservation>> GetClinicReservationsAsync(int virtualId, DateTime date);
		public Task<IEnumerable<Reservation>> GetUserReservationsAsync(int userId);
		public Task BookAppointmentAsync(Reservation reservation);
		public Task CancelAppointmentAsync(int reservationId);
		public Task UpdateAppointmentAsync(Reservation reservation);
		public Task<Reservation?> GetReservationByIdAsync(int reservationId);
		public Task PreviewReservation(int reservationId);
		public Task<IEnumerable<Reservation>> GetClinicReservationInMonth(int clinicId, DateTime date);
		//public Task<int> GetClinicReservationCapacity(int clinicId);
	}
}
