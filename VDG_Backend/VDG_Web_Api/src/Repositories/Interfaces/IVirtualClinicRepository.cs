using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces;

public interface IVirtualClinicRepository
{
	public Task AddClinic(VirtualClinic clinic, ClinicWorkTime initialWorkTime);
	public Task DeleteClinic(int clinicId);
	public Task UpdateClinic(VirtualClinic clinic);
	public Task<IEnumerable<VirtualClinic>> GetClinicsByDoctorId(int doctorId);
	public Task<VirtualClinic?> GetClinicById(int Id);
	public Task<IEnumerable<ClinicWorkTime>> GetClinicWorkTimes(int clinicId);
	public Task RemoveClinicWorkTime(int workTimeId);
	public Task AddClinicWorkTime(ClinicWorkTime workTime);
}