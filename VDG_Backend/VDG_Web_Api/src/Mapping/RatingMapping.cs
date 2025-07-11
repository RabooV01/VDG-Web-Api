using VDG_Web_Api.src.DTOs.RatingDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping
{
	public static class RatingMapping
	{
		public static Rating ToEntity(this RatingDTO ratingDTO)
			=> new()
			{
				Id = ratingDTO.Id,
				Act = ratingDTO.Act,
				AvgService = ratingDTO.AvgService,
				AvgWait = ratingDTO.AvgWait,
				DoctorId = ratingDTO.DoctorId,
				UserId = ratingDTO.UserId
			};
	}
}
