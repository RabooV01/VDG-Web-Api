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
        public static Rating AddRatingToEntity(this AddRatingDTO ratingDTO)
            => new()
            {
                Act = ratingDTO.Act,
                AvgService = ratingDTO.AvgService,
                AvgWait = ratingDTO.AvgWait,
                DoctorId = ratingDTO.DoctorId,
                UserId = ratingDTO.UserId
            };
        public static RatingDTO ToDto(this Rating rating)
            => new()
            {
                Id = rating.Id,
                Act = rating.Act,
                AvgService = rating.AvgService,
                AvgWait = rating.AvgWait,
                DoctorId = rating.DoctorId,
                UserId = rating.UserId
            };
    }
}
