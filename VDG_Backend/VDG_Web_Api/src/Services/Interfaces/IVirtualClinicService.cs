
using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Services.Interfaces;

public interface IVirtualClinicService
{
	public Task<VirtualClinicDTO> GetClinicById(int clinicId);
	public Task<VirtualClinicDTO> GetClinicsByDoctorId(int doctorId);
	public Task AddClinic(VirtualClinicDTO clinic);
	public Task<IEnumerable<ClinicWorkTimeDTO>> GetClinicWorkTimes(int clinicId);
	public Task AddClinicWorkTime(ClinicWorkTimeDTO workTimeDTO);
	public Task RemoveClinicWorkTime(int workTimeId);
	public Task UpdateClinic(VirtualClinicDTO clinicDTO);
	public Task DeleteClinic(int clinicId);
}