using VDG_Web_Api.src.DTOs.ReservationDTOs;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IReservationService
{
	public Task<IEnumerable<ClinicReservationDTO>> GetClinicReservationsAsync(int virtualClinicId, DateTime date);
	public Task<IEnumerable<UserReservationDTO>> GetUserReservationsAsync(int userId, DateOnly? date = null);
	public Task BookAppointmentAsync(ReservationDTO reservation);
	public Task CancelAppointmentAsync(int reservationId);
	public Task<bool> EditAppointmentAsync(ReservationDTO reservation);
	public Task PreviewReservation(int reservationId);
}