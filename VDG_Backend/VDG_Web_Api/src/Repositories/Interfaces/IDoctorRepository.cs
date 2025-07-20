using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface IDoctorRepository
	{
		public Task<int> AddDoctorAsync(Doctor doctor);
		public Task DeleteDoctorAsync(int doctorId);
		public Task UpdateDoctorAsync(Doctor doctor);
		public Task<Doctor?> GetDoctorByIdAsync(int id);
		public Task<Doctor?> GetDoctorBySyndicateIdAsync(string syndicateId);
		public Task UpdateDoctorDescription(string description, int doctorId);
		public Task UpdateDoctorSettings(int doctorId, TicketOptions ticketOptions, double ticketCost);

		// search items
		public Task<IEnumerable<Doctor>?> GetDoctorsBySpecialityIdAsync(int specialityId);
		public Task<IEnumerable<Doctor>?> GetDoctorsByNameAsync(string Name);
		public Task<IEnumerable<Doctor>?> GetDoctorsByGenderAsync(string gender);

	}
}
