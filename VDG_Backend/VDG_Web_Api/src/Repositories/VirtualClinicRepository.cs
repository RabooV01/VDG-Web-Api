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

	public async Task AddClinic(VirtualClinic clinic)
	{
		_context.VirtualClinics.Add(clinic);
		await _context.SaveChangesAsync();
	}

	public async Task AddClinicWorkTime(ClinicWorkTime workTime)
	{
		_context.ClinicWorkTimes.Add(workTime);
		await _context.SaveChangesAsync();
	}

	public async Task<VirtualClinic?> GetClinicById(int Id)
	{
		var clinic = await _context.VirtualClinics
			.Include(c => c.Doctor)
				.ThenInclude(d => d.User)
				.ThenInclude(u => u.Person)
			.Include(c => c.Doctor)
				.ThenInclude(d => d.Speciality)
			.Include(c => c.WorkTimes)
			.FirstOrDefaultAsync(c => c.Id == Id);
		return clinic;
	}

	public async Task<IEnumerable<VirtualClinic>> GetClinicsByDoctorId(int doctorId)
	{
		var doctorClinics = await _context.VirtualClinics
			.Include(c => c.WorkTimes)
			.Where(clinic => clinic.DoctorId.Equals(doctorId))
			.ToListAsync();

		return doctorClinics;
	}

	public async Task<IEnumerable<ClinicWorkTime>> GetClinicWorkTimes(int clinicId)
	{
		var workTimes = await _context.ClinicWorkTimes
			.Where(w => w.ClinicId == clinicId)
			.OrderBy(cw => cw.StartWorkHours)
			.ToListAsync();

		return workTimes;
	}

	public async Task DeleteClinic(int clinicId)
	{
		var clinic = await GetClinicById(clinicId);
		if (clinic == null)
			return;

		_context.VirtualClinics.Remove(clinic);
		await _context.SaveChangesAsync();
	}

	public async Task RemoveClinicWorkTime(int workTimeId)
	{
		var clinicWorkTime = await _context.ClinicWorkTimes.FindAsync(workTimeId);

		if (clinicWorkTime == null)
			return;

		_context.ClinicWorkTimes.Remove(clinicWorkTime);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateClinic(VirtualClinic clinic)
	{
		_context.VirtualClinics.Update(clinic);
		await _context.SaveChangesAsync();
	}
}
