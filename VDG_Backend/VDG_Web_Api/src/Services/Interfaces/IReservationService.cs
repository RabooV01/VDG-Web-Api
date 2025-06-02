using VDG_Web_Api.src.DTOs.ReservationDTOs;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IReservationRepository
{
    public Task<IEnumerable<ClinicReservation>> GetClinicReservationsAsync(int virtualId, DateOnly? date = null);
    public Task<IEnumerable<UserReservation>> GetUserReservationsAsync(int userId, DateOnly? date = null);
    public Task BookAppointmentAsync(ReservationDTO reservation);
    public Task BookRevisionAppointmentAsync(ReservationDTO reservation);
    public Task CancelAppointmentAsync(int reservationId);
    public Task EditAppointmentAsync(ReservationDTO reservation);
}