using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories;

public class ReservationRepository : IReservationRepository
{
    public Task BookAppointment(User user, VirtualClinic clinic, DateTime date)
    {
        throw new NotImplementedException();
    }

    public Task BookRevisionAppointment(User user, VirtualClinic clinic, DateTime date)
    {
        throw new NotImplementedException();
    }

    public Task CancelAppointment(int reservationId)
    {
        throw new NotImplementedException();
    }

    public Task<Reservation?> UpdateAppointment(Reservation reservation)
    {
        throw new NotImplementedException();
    }
}