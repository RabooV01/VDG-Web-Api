
using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IVirtualClinicService 
{
    public Task<VirtualClinicDTO> GetClinicById(int clinicId);
}