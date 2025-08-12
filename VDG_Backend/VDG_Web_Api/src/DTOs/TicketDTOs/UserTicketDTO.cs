namespace VDG_Web_Api.src.DTOs.TicketDTOs
{
	public class UserTicketDTO : TicketDTO
	{
		public string DoctorName { get; set; } = string.Empty;
		public string DoctorSpeciality { get; set; } = string.Empty;
	}
}
