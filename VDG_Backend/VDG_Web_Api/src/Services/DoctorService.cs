using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
	public class DoctorService : IDoctorService
	{
		private readonly IDoctorRepository _doctorRepository;
		public DoctorService(IDoctorRepository doctorRepository)
		{
			_doctorRepository = doctorRepository;
		}

		public async Task AddDoctor(AddDoctorDTO doctorDTO)
		{
			try
			{
				var doctor = await _doctorRepository.AddDoctorAsync(doctorDTO.ToEntity());
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task DeleteDoctor(int doctorId)
		{
			try
			{
				await _doctorRepository.DeleteDoctorAsync(doctorId);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<DoctorDTO> GetDoctorById(int doctorId)
		{
			var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);

			if (doctor == null)
			{
				throw new KeyNotFoundException("No such doctor");
			}

			return doctor.ToDto();
		}

		public async Task UpdateDoctorConsultationSettings(DoctorSettings doctorSettings, int doctorId)
		{
			try
			{
				await _doctorRepository.UpdateDoctorSettings(doctorId, doctorSettings.TicketOption, doctorSettings.TicketCost);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task UpdateDoctorDescription(int doctorId, string description)
		{
			try
			{
				await _doctorRepository.UpdateDoctorDescription(description, doctorId);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
