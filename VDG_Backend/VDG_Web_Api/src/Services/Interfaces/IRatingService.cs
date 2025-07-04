using VDG_Web_Api.src.DTOs.RatingDTOs;

namespace VDG_Web_Api.src.Services.Interfaces
{
    public interface IRatingService
    {
        public Task Rate(RatingDTO ratingDto);
    }
}
