using VDG_Web_Api.src.DTOs.ReservationDTOs;
using VDG_Web_Api.src.Extensions.Validation;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Services.Interfaces;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IVirtualClinicService _virtualClinicService;
    private readonly IUserService _userService;

    private readonly static int _confirmThresholdPerHours = 24;

	public ReservationService(IReservationRepository reservationRepository,
		IVirtualClinicService virtualClinicService,
		IUserService userService)
	{
		_reservationRepository = reservationRepository;
		_virtualClinicService = virtualClinicService;
		_userService = userService;
	}

    private Reservation MapToEntity(ReservationDTO Dto)
    {
        return new Reservation()
        {
            Id = Dto.Id,
            UserId = Dto.UserId,
            VirtualId = Dto.VirtualId,
            Text = Dto.Text,
            ScheduledAt = Dto.ScheduledAt,
            Type = Dto.Type
        };
    }
    public async Task<UserReservationDTO> MapToUserReservation(ReservationDTO Dto)
    {
        if (Dto.VirtualId is null)
            throw new ArgumentNullException("Virtual clinic is not selected.");

        var virtualClinic = await _virtualClinicService.GetClinicById(Dto.VirtualId.Value);
        var userReservation = new UserReservationDTO()
        {
            ReservationDto = Dto,
            VirtualDto = virtualClinic
        };
        return userReservation;
    }

    public async Task<ClinicReservationDTO> MapToClinicReservation(ReservationDTO Dto)
    {
        if (Dto.UserId is null)
            throw new ArgumentNullException("User is not selected.");

        var userDto = await _userService.GetUser(Dto.UserId.Value);

        var userReservation = new ClinicReservationDTO()
        {
            ReservationDto = Dto,
            UserDto = userDto
        };

        return userReservation;
    }

    public ReservationDTO MapToDto(Reservation reservation) => new ReservationDTO()
    {
        Id = reservation.Id,
        Type = reservation.Type,
        Text = reservation.Text,
        ScheduledAt = reservation.ScheduledAt,
        UserId = reservation.UserId,
        VirtualId = reservation.VirtualId
    };

    public async Task BookAppointmentAsync(ReservationDTO reservationDto)
    {
        if (!reservationDto.IsValidReservation())
        {
            throw new ArgumentNullException("Reservation is invalid");
        }

        Reservation reservation = MapToEntity(reservationDto);

        var userReservations = await GetUserReservationsAsync(reservation.UserId!.Value);

        if(userReservations.Select(r => r.VirtualDto!.DoctorId!.Value).Contains(reservation.Virtual!.DoctorId!.Value))
        {
            throw new InvalidOperationException("Appointment has not been reserved because an there is an exist one with same doctor");
        }
        
        // TODO check same doctor
        try
        {
            await _reservationRepository.BookAppointmentAsync(reservation);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Error occured while booking the appointment: {ex.Message}", ex);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Unexpected error occurred: {e.Message}", e);
        }
    }

    public async Task CancelAppointmentAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);

        if(reservation == null)
        {
            throw new KeyNotFoundException("No such reservation is found.");
        }

        if(reservation.ScheduledAt.Subtract(DateTime.Now).Hours < _confirmThresholdPerHours)
        {
            throw new ArgumentException("Appointment has been confirmed and cannot be canceled");   
        }

        try
        {
            await _reservationRepository.CancelAppointmentAsync(reservationId);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException($"Unable to cancel appointment: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error occured while canceling the appointment: {ex.Message}", ex);
        }
    }

    public async Task<ReservationDTO> GenerateClinicAvailableReservations(IEnumerable<Reservation> busyAppointments, int virtualId, DateOnly date)
    {
        var clinic = await _virtualClinicService.GetClinicById(virtualId);
        // TODO: get ranges, and total appointments on them, then add all into one list 
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ClinicReservationDTO>> GetClinicReservationsAsync(int virtualId, DateOnly? date = null)
    {
        try
        {
            var reservations = await _reservationRepository.GetClinicReservationsAsync(virtualId: virtualId, day: date);

            var userIds = reservations.Select(r => r.UserId)
            .Distinct()
            .ToList();

            var userDtos = userIds.Where(Id => Id != null).Select(Id => _userService.GetUser(Id!.Value).Result);

            return reservations.Select(s =>
            {
                var reservationDto = MapToDto(s);

                var userDto = userDtos.FirstOrDefault(user => user?.Id == reservationDto.UserId);

                return new ClinicReservationDTO()
                {
                    ReservationDto = reservationDto,
                    UserDto = userDto
                };
            }).ToList();
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<UserReservationDTO>> GetUserReservationsAsync(int userId, DateOnly? date = null)
    {
        try
        {
            var reservations = await _reservationRepository.GetUserReservationsAsync(userId);

            var ClinicIds = reservations.Select(r => r.VirtualId!.Value);

            var ClinicDtos = ClinicIds.Select(c => _virtualClinicService.GetClinicById(c).Result);

            return reservations.Select(r =>
            {
                var reservationDto = MapToDto(r);

                var virtualClinicDto = ClinicDtos.FirstOrDefault(c => c.Id == r.VirtualId);

                return new UserReservationDTO()
                {
                    ReservationDto = reservationDto,
                    VirtualDto = virtualClinicDto
                };
            });
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Could not retrive data, Error: {ex.Message}", ex);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task EditAppointmentAsync(ReservationDTO reservationDto)
    {
        if (!reservationDto.IsValidReservation())
        {
            throw new ArgumentException("Reservation value is invalid, No update were applied.");
        }

        Reservation reservation = MapToEntity(reservationDto);

        try
        {
            await _reservationRepository.UpdateAppointmentAsync(reservation);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Update failed, Error: {ex.Message}", ex);
        }
        catch (Exception)
        {
            throw;
        }
    }
}