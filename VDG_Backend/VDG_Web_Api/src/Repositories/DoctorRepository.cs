using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
        public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
        {
            Doctor? doctor = await _context.Doctors.Include(d => d.Speciality)
                .Include(d => d.User)
                .ThenInclude(u => u.Person)
                .FirstOrDefaultAsync(d => d.Id == doctorId);

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
        public async Task<int> GetRatingDoctorByIdAsync(int DoctorId)
        {
            if (await _context.Ratings.AnyAsync() is false)
                return 0;

            var avgWait = await _context.Ratings.Where(r => r.DoctorId == DoctorId).AverageAsync(r => r.AvgWait);
            var avgService = await _context.Ratings.Where(r => r.DoctorId == DoctorId).AverageAsync(r => r.AvgService);
            var act = await _context.Ratings.Where(r => r.DoctorId == DoctorId).AverageAsync(r => r.Act);
            return (int)Math.Round((avgWait + avgService + act) / 3.0);
        }
        public async Task<IEnumerable<Doctor>?> GetDoctorsByRatingAsync(int rating)
        {
            var doctors = new List<Doctor>();
            foreach (var doctor in _context.Doctors)
            {
                if (await GetRatingDoctorByIdAsync(doctor.Id) == rating)
                    doctors.Add(doctor);
            }
            return doctors;
        }



        public async Task<Doctor?> GetDoctorBySyndicateIdAsync(string syndicateId)
        {

            Doctor? doctor = await _context.Doctors.FindAsync(syndicateId);
            if (doctor == null)
                throw new ArgumentNullException("this Doctor is not found");

            return doctor;
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

        public async Task<Doctor?> GetDoctorByUserId(int userId)
        {
            try
            {
                var doctor = await _context.Doctors.Include(d => d.User)
                    .FirstOrDefaultAsync(d => d.UserId == userId);
                return doctor;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IEnumerable<Doctor>> GetDoctors(int page, int pageSize, int? specialityId = null, string? name = null)
        {
            try
            {
                Expression<Func<Doctor, bool>> doctorFilterExpression = doctor => (specialityId == null || doctor.SpecialityId == specialityId) &&
                (name == null || $"{doctor.User.Person.FirstName} {doctor.User.Person.LastName}".Contains(name));

                var doctors = await _context.Doctors.Include(d => d.User)
                    .ThenInclude(u => u.Person)
                    .Include(d => d.Speciality)
                    .Where(doctorFilterExpression)
                    .OrderByDescending(doctor => doctor.User.Person.FirstName)
                    .ThenBy(doctor => doctor.User.Person.LastName)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return doctors;
            }
            catch (Exception e)
            {
                throw new Exception("Error while retrieving data.", e);
            }
        }





    }
}
/*
 // افترض أن لديك هذه الكيانات
public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Rating
{
    public int DoctorId { get; set; }
    public double Score { get; set; }
}

// ثم الافتراض بأن لديك IEnumerable للطبيب وIEnumerable للتقييم
IEnumerable<Doctor> doctors = ...; // قائمة الأطباء
IEnumerable<Rating> ratings = ...; // قائمة التقييمات

// لحساب معدل كل دكتور
var averageRatings = from doctor in doctors
                     join rating in ratings 
                     on doctor.Id equals rating.DoctorId into doctorRatings
                     select new
                     {
                         Doctor = doctor.Name,
                         AverageRating = doctorRatings.Any() ? doctorRatings.Average(r => r.Score) : 0
                     };

// الآن يمكنك استعراض النتائج
foreach (var item in averageRatings)
{
    Console.WriteLine($"الدكتور: {item.Doctor}, المتوسط: {item.AverageRating}");
}

var averageRatings = doctors.GroupJoin(
    ratings,                          // المصدر الثاني الذي نربطه (التقييمات)
    doctor => doctor.Id,              // المفتاح من القائمة الأولى (معرف الدكتور)
    rating => rating.DoctorId,        // المفتاح من القائمة الثانية (معرف الدكتور في التقييم)
    (doctor, doctorRatings) => new    // العملية التي تنفذ لكل دكتور مع تقييماته المرتبطة
    {
        Doctor = doctor.Name,         // اسم الدكتور
        AverageRating = doctorRatings.Any() ? doctorRatings.Average(r => r.Score) : 0 // حساب المتوسط
    });

 */