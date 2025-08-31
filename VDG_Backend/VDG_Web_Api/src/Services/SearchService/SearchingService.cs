using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.DTOs.FilterDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Services.SearchService
{
	public class SearchingService : ISearchingService
	{
		private readonly IDoctorRepository _doctorRepository;
		//private readonly IVirtualClinicRepository _VirtualRepository;
		public SearchingService(IDoctorRepository doctorRepository/*, IVirtualClinicRepository virtualRepository*/)
		{
			_doctorRepository = doctorRepository;
			//_VirtualRepository = virtualRepository;
		}

		public async Task<IEnumerable<DoctorSearchDto>> GetByName(string name)
		{
			try
			{
				var doctors = await _doctorRepository.GetDoctorsByNameAsync(name);
				return doctors.Select(d => d.ToSearchDto(null));
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<IEnumerable<DoctorSearchDto>> SearchDoctorAsync(FilterDTO filter)
		{
			var filteredDoctors = await _doctorRepository.GetDoctorsBySpecialityIdAsync(filter.SpecialityId);

			if (!string.IsNullOrEmpty(filter.Gender))
			{
				filteredDoctors = filteredDoctors.Where(d => filter.Gender.Equals(d.User.Person.Gender, StringComparison.OrdinalIgnoreCase));
			}

			if (filter.MinRate is not null)
			{
				filteredDoctors = filteredDoctors.Where(d => d.Ratings.Sum(r => (r.AvgService + r.Act + r.AvgWait) / 3) / d.Ratings.Count >= filter.MinRate);
			}

			filteredDoctors = filteredDoctors.Where(d => d.VirtualClinics.Any(vc => vc.PreviewCost <= filter.CostRange));
			if (filter.ShortestDistanceFirst)
			{
				filteredDoctors = filteredDoctors.OrderBy(d => d.VirtualClinics.Select(vc => ClinicDistance(vc, filter.UserLat!.Value, filter.UserLon!.Value)));
			}

			return filteredDoctors.Select(doctor =>
			{
				var minClinic = filter.ShortestDistanceFirst ? doctor.VirtualClinics.MinBy(vc => ClinicDistance(vc, filter.UserLat!.Value, filter.UserLon!.Value)) : null;

				return doctor.ToSearchDto(minClinic);
			});
		}

		private double ClinicDistance(VirtualClinic vc, double userLat, double userLon)
		{
			if (vc.LocationCoords == null)
			{
				return int.MaxValue;
			}
			var coords = vc.LocationCoords.Split(',');
			var lat = double.Parse(coords[0]);
			var lon = double.Parse(coords[1]);
			var dist = HaversineDistance(userLat, userLon, lat, lon);
			return dist;
		}

		private double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
		{
			double ToRadians(double angle) => Math.PI * angle / 180.0;

			double R = 6371; // Earth radius in km
			double dLat = ToRadians(lat2 - lat1);
			double dLon = ToRadians(lon2 - lon1);

			double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
					   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
					   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

			double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

			return R * c; // Distance in kilometers
		}


		private double EuclideanDistance(double lat1, double lon1, double lat2, double lon2)
		{
			double dLat = lat2 - lat1;
			double dLon = lon2 - lon1;

			return Math.Sqrt(dLat * dLat + dLon * dLon);
		}
	}
}
