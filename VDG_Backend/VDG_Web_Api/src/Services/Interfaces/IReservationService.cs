using VDG_Web_Api.src.DTOs.ReservationDTOs;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IReservationService
{
	public Task<IEnumerable<ClinicReservationDTO>> GetClinicReservationsAsync(int virtualId, DateOnly date);
	public Task<IEnumerable<UserReservationDTO>> GetUserReservationsAsync(int userId, DateOnly? date = null);
	public Task BookAppointmentAsync(ReservationDTO reservation);
	public Task CancelAppointmentAsync(int reservationId);
	public Task EditAppointmentAsync(ReservationDTO reservation);
}