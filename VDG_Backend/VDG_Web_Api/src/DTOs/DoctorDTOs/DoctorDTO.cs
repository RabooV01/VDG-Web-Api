namespace VDG_Web_Api.src.DTOs.DoctorDTOs
{
	public class DoctorDTO
	{
		public int Id { get; set; }
		public string SyndicateId { get; set; } = string.Empty;

		public int? UserId { get; set; }

		public int? SpecialityId { get; set; }

		public string Speciality { get; set; } = string.Empty;

	}
}
