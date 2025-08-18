using VDG_Web_Api.src.DTOs.SpecialityDTOS;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping
{
	public static class SpecialityMapping
	{
		public static SpecialityDTO ToDto(this Speciality speciality)
			=> new()
			{
				Speciality = speciality.Name,
				Id = speciality.Id
			};
	}
}
