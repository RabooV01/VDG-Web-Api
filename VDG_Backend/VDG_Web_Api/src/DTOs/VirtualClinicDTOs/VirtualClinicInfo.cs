using VDG_Web_Api.src.DTOs.DoctorDTOs;

namespace VDG_Web_Api.src.DTOs.VirtualClinicDTOs
{
	public class VirtualClinicInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public DoctorInfo Doctor { get; set; } = null!;
	}
}
