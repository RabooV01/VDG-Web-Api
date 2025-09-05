namespace VDG_Web_Api.src.DTOs.FilterDTOs
{
	public class FilterDTO
	{
		public int SpecialityId { get; set; }
		public string? Gender { get; set; }
		public double CostRange { get; set; } = 1e10;
		public double? MinRate { get; set; }
		public bool ShortestDistanceFirst { get; set; }
		public double? UserLat { get; set; }
		public double? UserLon { get; set; }

	}
}
