using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories;

public class VirtualClinicRepository : IVirtualClinicRepository
{
	private readonly VdgDbDemoContext _context;

	public VirtualClinicRepository(VdgDbDemoContext context)
	{
		_context = context;
	}

	public async Task AddClinic(VirtualClinic clinic, ClinicWorkTime initialWorkTime)
	{
		_context.VirtualClinics.Add(clinic);
		await _context.SaveChangesAsync();

		int clinicId = clinic.Id;

		initialWorkTime.ClinicId = clinicId;

		await AddClinicWorkTime(initialWorkTime);
	}

	public async Task AddClinicWorkTime(ClinicWorkTime workTime)
	{
		_context.ClinicWorkTimes.Add(workTime);
		await _context.SaveChangesAsync();
	}

	public async Task<VirtualClinic?> GetClinicById(int Id)
	{
		var clinic = await _context.VirtualClinics.FindAsync(Id);
		return clinic;
	}

	public async Task<IEnumerable<VirtualClinic>> GetClinicsByDoctorId(int doctorId)
	{
		var doctorClinics = _context.VirtualClinics
			.Include(x => x.Doctor)
				.ThenInclude(d => d.User)
				.ThenInclude(u => u.Person)
			.Where(clinic => clinic.DoctorId.Equals(doctorId));
		return await doctorClinics.ToListAsync();
	}

	public async Task<IEnumerable<ClinicWorkTime>> GetClinicWorkTimes(int clinicId)
	{
		var workTimes = await _context.ClinicWorkTimes.Where(w => w.ClinicId == clinicId).ToListAsync();
		return workTimes;
	}

	public async Task RemoveClinic(int clinicId)
	{
		var clinic = await GetClinicById(clinicId);
		if (clinic != null)
		{
			_context.VirtualClinics.Remove(clinic);
			await _context.SaveChangesAsync();
		}
	}

	public async Task RemoveClinicWorkTime(int workTimeId)
	{
		var clinicWorkTime = await _context.ClinicWorkTimes.FindAsync(workTimeId);
		if (clinicWorkTime != null)
			await _context.ClinicWorkTimes.Where(c => c.Id == workTimeId).ExecuteDeleteAsync();
	}

	public async Task UpdateClinic(VirtualClinic clinic)
	{
		_context.VirtualClinics.Update(clinic);
		await _context.SaveChangesAsync();
	}
}
