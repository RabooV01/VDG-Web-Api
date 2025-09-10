using VDG_Web_Api.src.DTOs.RatingDTOs;

namespace VDG_Web_Api.src.Services.Interfaces
{
    public interface IRatingService
    {
        public Task Rate(AddRatingDTO ratingDto);
        public Task Update(UpdateRatingDTO ratingDto);
        public Task Delete(int rateId);
        public Task<IEnumerable<RatingDTO>> Get(int id);
    }
}
