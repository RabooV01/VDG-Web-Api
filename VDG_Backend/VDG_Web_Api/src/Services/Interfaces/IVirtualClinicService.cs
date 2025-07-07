
using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IVirtualClinicService
{
	public Task<VirtualClinicDTO> GetClinicById(int clinicId);
	public Task<VirtualClinicInProfileDTO> GetClinicsByDoctorId(int doctorId);
	public Task<IEnumerable<ClinicWorkTimeDTO>> GetClinicWorkTimes(int clinicId);
	public Task AddClinic(AddVirtualClinicDTO clinic);
	public Task AddClinicWorkTime(ClinicWorkTimeDTO workTimeDTO);
	public Task RemoveClinicWorkTime(int workTimeId);
	public Task UpdateClinic(UpdateVirtualClinicDTO clinicDTO);
	public Task DeleteClinic(int clinicId);
}