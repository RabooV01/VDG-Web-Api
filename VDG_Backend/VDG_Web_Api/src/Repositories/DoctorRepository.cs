using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
namespace VDG_Web_Api.src.Repositories
{
	public class DoctorRepository : IDoctorRepository
	{
		private readonly VdgDbDemoContext _context;
		public DoctorRepository(VdgDbDemoContext context)
		{
			_context = context;
		}

		public async Task<int> AddDoctorAsync(Doctor doctor)
		{
			try
			{
				_context.Doctors.Add(doctor);
				await _context.SaveChangesAsync();
				return doctor.Id;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Unable to ِadd Doctor,error:[{ex.Message}]", ex);
			}

		}

		public async Task DeleteDoctorAsync(int doctorId)
		{
			Doctor? doctor = await _context.Doctors.FindAsync(doctorId);
			if (doctor == null)
			{
				throw new ArgumentNullException("actually, this doctorId is not found");
			}
			try
			{
				_context.Doctors.Remove(doctor);
				await _context.SaveChangesAsync();

			}
			catch (Exception ex)
			{
				throw new InvalidOperationException($"Unable to remove this doctor, Error:[{ex.Message}]", ex);
			}
		}

		public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
		{
			Doctor? doctor = await _context.Doctors.Include(d => d.Speciality)
				.Include(d => d.User)
				.ThenInclude(u => u.Person)
				.FirstOrDefaultAsync(d => d.Id == doctorId);
			if (doctor == null)
				throw new ArgumentNullException("this Doctor is not found");

			return doctor;
		}

		public async Task<IEnumerable<Doctor>?> GetDoctorsByNameAsync(string Name)
		{
			var doctors = await _context.Doctors.Include(d => d.User)
				.ThenInclude(u => u.Person)
				.Where(d => ($"{d.User!.Person!.FirstName} {d.User.Person.LastName}").Contains(Name))
				.ToListAsync();

			if (doctors == null)
			{
				throw new ArgumentException($"there is no the doctor with the name:{Name}");
			}
			return doctors;
		}

		public async Task UpdateDoctorAsync(Doctor doctor)
		{
			var doctorToUpdate = await _context.Doctors.FindAsync(doctor.Id);

			if (doctorToUpdate == null)
				throw new KeyNotFoundException("the doctor has not found for update");

			doctorToUpdate = doctor;
			try
			{
				_context.Update(doctorToUpdate);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{

				throw new InvalidOperationException($"Unable to update the doctor ,error: {ex.Message}", ex);
			}
			throw new NotImplementedException();

		}


		public async Task<IEnumerable<Doctor>?> GetDoctorsByGenderAsync(string gender)
		{
			var doctors = await _context.Doctors.Include(d => d.User)
				.ThenInclude(u => u.Person)
				.Where(d => d.User.Person.Gender == gender)
				.ToListAsync();

			if (doctors == null)
				throw new KeyNotFoundException($"There are no {gender} doctors ");

			return doctors;
		}

		public async Task<IEnumerable<Doctor>?> GetDoctorsBySpecialityIdAsync(int specialityId)
		{
			var doctors = await _context.Doctors.Include(d => d.Speciality)
				.Where(d => d.Speciality.Id == specialityId)
				.ToListAsync();

			return doctors;
		}


		public async Task<Doctor?> GetDoctorBySyndicateIdAsync(string syndicateId)
		{

			Doctor? doctor = await _context.Doctors.FindAsync(syndicateId);
			if (doctor == null)
				throw new ArgumentNullException("this Doctor is not found");

			return doctor;
		}

		public async Task UpdateDoctorDescription(string description, int doctorId)
		{
			try
			{
				await _context.Doctors.Where(d => d.Id == doctorId)
					.ExecuteUpdateAsync(d => d.SetProperty(p => p.Description, description));
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task UpdateDoctorSettings(int doctorId, TicketOptions ticketOptions, double ticketCost)
		{
			try
			{
				await _context.Doctors.Where(d => d.Id == doctorId)
					.ExecuteUpdateAsync(d => d.SetProperty(p => p.TicketOption, ticketOptions)
					.SetProperty(p => p.TicketCost, ticketCost));
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
