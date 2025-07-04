using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping;

public static class DoctorMapping
{
    public static Doctor ToEntity(this DoctorDTO doctorDTO)
        => new()
        {
            Id = doctorDTO.Id,
            UserId = doctorDTO.UserId,
            SpecialityId = doctorDTO.SpecialityId
        };
}