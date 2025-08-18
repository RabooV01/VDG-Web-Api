using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.DTOs.DoctorDTOs
{
	public class AddDoctorDTO
	{
		public int UserId { get; set; }
		public string Description { get; set; } = string.Empty;
		public int SpecialityId { get; set; }
		public string SyndicateId { get; set; } = string.Empty;
		public TicketOptions TicketOptions { get; set; }
		public double TicketCost { get; set; }

	}
}
