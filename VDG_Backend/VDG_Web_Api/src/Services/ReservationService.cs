using VDG_Web_Api.src.DTOs.ReservationDTOs;

namespace VDG_Web_Api.src.Services.Interfaces;

public class ReservationRepository : IReservationRepository
{
    public Task BookAppointmentAsync(ReservationDTO reservation)
    {
        throw new NotImplementedException();
    }

    public Task BookRevisionAppointmentAsync(ReservationDTO reservation)
    {
        throw new NotImplementedException();
    }

    public Task CancelAppointmentAsync(int reservationId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClinicReservation>> GetClinicReservationsAsync(int virtualId, DateOnly? date = null)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserReservation>> GetUserReservationsAsync(int userId, DateOnly? date = null)
    {
        throw new NotImplementedException();
    }

    public Task EditAppointmentAsync(ReservationDTO reservation)
    {
        throw new NotImplementedException();
    }
}