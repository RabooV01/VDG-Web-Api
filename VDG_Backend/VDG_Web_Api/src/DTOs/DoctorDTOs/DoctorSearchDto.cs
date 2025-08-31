using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;

namespace VDG_Web_Api.src.DTOs.DoctorDTOs
{
	public class DoctorSearchDto
	{
		public int DoctorId { get; set; }
		public string DoctorName { get; set; }
		public string DoctorDescription { get; set; }
		public string TicketOption { get; set; }
		public double TicketCost { get; set; }
		public double Rating { get; set; }
		public string ShortestDistanceClinic { get; set; }
		public string ShortestDistanceLocation { get; set; }
		public IEnumerable<VirtualClinicSearchDto> Clinics { get; set; } = null!;
	}
}
