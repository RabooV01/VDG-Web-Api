namespace VDG_Web_Api.src.Services.LocalizationService
{
    public class LocalizationService : ILocalizationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://nominatim.openstreetmap.org/search";
        private readonly string _apiReverseUrl = "https://nominatim.openstreetmap.org/reverse";
        public LocalizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LocationDto> GetLocationAsync(string locationName)
        {
            var query = locationName.Split([' ', '/', '-', '_']);
            var encodedName = Uri.EscapeDataString(query[0]);

            foreach (var q in query)
            {
                if (q == query[0])
                    continue;
                encodedName = $"{encodedName}+{Uri.EscapeDataString(q)}";
            }
            var url = $"{_apiUrl}?q={encodedName}&format=json&limit=1";
            if (!_httpClient.DefaultRequestHeaders.UserAgent.Any())
            {
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("VDG-Backend");
            }
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    // Handle error or throw
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();

                var results = System.Text.Json.JsonSerializer.Deserialize<List<LocationDto>>(json);

                if (results == null || results.Count == 0)
                {
                    throw new ArgumentException("No such location has been found.", nameof(locationName));
                }

                return results.Select(r =>
                {
                    var name = r.DisplayName.Split(',');
                    var dname = name[0];
                    r.DisplayName = name[1] == null ? dname : $"{dname}, {name[1]}";
                    return r;
                }).First();
            }
            catch (HttpRequestException ex)
            {
                // Handle network errors here
                throw new InvalidOperationException("Error connecting to location service.", ex);
            }


        }

        public async Task<LocationDto> GetLocationAsync(double latitude, double longitude)
        {

            var url = $"{_apiReverseUrl}?lat={latitude}&lon={longitude}&format=json&limit=1";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException("No such location has been found.", "coordinates");
            }

            var json = await response.Content.ReadAsStringAsync();

            var results = System.Text.Json.JsonSerializer.Deserialize<List<LocationDto>>(json);

            if (results == null || results.Count == 0)
            {
                throw new ArgumentException("No such location has been found.", "coordinates");
            }

            return results.First();
        }
        public async Task<double> EuclideanDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            return Math.Sqrt(dLat * dLat + dLon * dLon);
        }

        public async Task<double> HaversineDistance(double lat1, double lon1, double lat2, double lon2)
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
    }
}
