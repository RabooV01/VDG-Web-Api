namespace VDG_Web_Api.src.DTOs.FilterDTOs
{
    public class FilterDTO
    {
        public string? Name { get; set; }
        public int specialityId { get; set; }
        public string? gender { get; set; }
        public double? range { get; set; } = 1e10;
        public int? rating { get; set; }
        public double? locationX { get; set; }
        public double? locationY { get; set; }

    }
}
