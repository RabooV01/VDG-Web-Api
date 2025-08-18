namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs
{
	public class WorkTimeInitialize
	{
		public int ClinicId { get; set; }
		public TimeOnly StartWorkHours { get; set; }
		public TimeOnly EndWorkHours { get; set; }
		public List<DayOfWeek> Holidays { get; set; } = new();
	}
}
