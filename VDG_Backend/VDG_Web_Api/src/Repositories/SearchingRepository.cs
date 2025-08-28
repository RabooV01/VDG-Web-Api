using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.DTOs.FilterDTOs;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class SearchingRepository : ISearchingRepository
    {
        private readonly VdgDbDemoContext _context;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IVirtualClinicRepository _VirtualRepository;
        public SearchingRepository(VdgDbDemoContext context, IDoctorRepository doctorRepository, IVirtualClinicRepository virtualRepository)
        {
            _context = context;
            _doctorRepository = doctorRepository;
            _VirtualRepository = virtualRepository;
        }

        public async Task<IEnumerable<Doctor>> SearchDoctorAsync(FilterDTO filter)
        {
            // search by name
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var doctors = await _doctorRepository.GetDoctorsByNameAsync(filter.Name);

                //var virs = new List<VirtualClinic>();

                //foreach (var el in doctors){
                //    var clinics = await _VirtualRepository.GetClinicsByDoctorId(el.Id);
                //    virs.AddRange(clinics);
                //}
                //return virs;

                return doctors;
            }

            //else search by filter

            var filteredDoctors = await _doctorRepository.GetDoctorsBySpecialityIdAsync(filter.specialityId);


            if (!string.IsNullOrEmpty(filter.gender))
            {
                var doctorsByGender = await _doctorRepository.GetDoctorsByGenderAsync(filter.gender);
                filteredDoctors = filteredDoctors.Where(d => doctorsByGender.Any(g => g.Id == d.Id));
            }

            IEnumerable<Doctor> doctorsByRating = Enumerable.Empty<Doctor>();
            if (filter.rating is not null)
            {
                doctorsByRating = await _doctorRepository.GetDoctorsByRatingAsync((int)filter.rating);
                filteredDoctors = filteredDoctors.Where(d => doctorsByRating.Any(r => r.Id == d.Id));

            }

            var WantedDoctors = new List<Doctor>();

            foreach (var doctor in filteredDoctors)
            {
                var clinics = await _VirtualRepository.GetClinicsByDoctorId(doctor.Id);

                foreach (var clinic in clinics)
                {
                    if (clinic.PreviewCost <= filter.range /* &&  do Location */)
                    {
                        WantedDoctors.Add(doctor);
                        break;
                    }
                }
            }

            return WantedDoctors;
        }
    }
}
