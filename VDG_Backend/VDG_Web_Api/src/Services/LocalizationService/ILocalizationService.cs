namespace VDG_Web_Api.src.Services.LocalizationService
{
    public interface ILocalizationService
    {
        Task<LocationDto> GetLocationAsync(string name);
        Task<LocationDto> GetLocationAsync(double latitude, double longitude);
        Task<double> HaversineDistance(double lat1, double lon1, double lat2, double lon2);
        Task<double> EuclideanDistance(double lat1, double lon1, double lat2, double lon2);

    }
}
