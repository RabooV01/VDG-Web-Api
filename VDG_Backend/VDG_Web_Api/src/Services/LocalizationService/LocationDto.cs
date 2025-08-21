using System.Text.Json.Serialization;

namespace VDG_Web_Api.src.Services.LocalizationService
{
	public class LocationDto
	{
		[JsonPropertyName("display_name")]
		public string DisplayName { get; set; }

		[JsonPropertyName("lat")]
		public string Lat { get; set; }

		[JsonPropertyName("lon")]
		public string Lon { get; set; }
	}
}
