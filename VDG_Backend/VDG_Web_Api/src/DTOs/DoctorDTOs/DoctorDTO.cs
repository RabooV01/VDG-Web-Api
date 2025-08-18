using VDG_Web_Api.src.DTOs.UserDTOs;

namespace VDG_Web_Api.src.DTOs.DoctorDTOs
{
	public class DoctorDTO : UserDTO
	{
		public int DoctorId { get; set; }

		public int SpecialityId { get; set; }

		public string Speciality { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string TicketOption { get; set; } = string.Empty;
		public double TicketCost { get; set; }
	}
}
