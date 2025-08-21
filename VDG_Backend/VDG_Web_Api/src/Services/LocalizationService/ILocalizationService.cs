namespace VDG_Web_Api.src.Services.LocalizationService
{
	public interface ILocalizationService
	{
		Task<LocationDto> GetLocationAsync(string name);
		Task<LocationDto> GetLocationAsync(double latitude, double longitude);
	}
}
