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


    }

    public Task AddClinicWorkTime(ClinicWorkTime workTime)
    {
        throw new NotImplementedException();
    }

    public Task<VirtualClinic?> GetClinicById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VirtualClinic>> GetClinicsByDoctorId(int doctorId)
    {
        throw new NotImplementedException();
    }

    public Task<ClinicWorkTime> GetClinicWorkTimes(int clinicId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveClinic(int clinicId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveClinicWorkTime(int workTimeId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateClinic(VirtualClinic clinic)
    {
        throw new NotImplementedException();
    }
}
