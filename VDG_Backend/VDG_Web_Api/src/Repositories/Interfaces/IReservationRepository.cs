using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        public Task BookAppointment(User user, VirtualClinic clinic, DateTime date);
        public Task BookRevisionAppointment(User user, VirtualClinic clinic, DateTime date);
        public Task CancelAppointment(int reservationId);
        public Task<Reservation?> UpdateAppointment(Reservation reservation);
    }
}