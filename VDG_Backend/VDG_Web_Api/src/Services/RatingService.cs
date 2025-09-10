using VDG_Web_Api.src.DTOs.RatingDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository repository)
        {
            _ratingRepository = repository;
        }

        public async Task<IEnumerable<RatingDTO>> Get(int id)
        {
            try
            {
                var ratings = await _ratingRepository.GetRate(id);
                if (ratings == null)
                {
                    throw new ArgumentNullException(nameof(ratings));
                }

                return ratings.Select(r => r.ToDto());
            }
            catch (Exception ex)
            {

                throw new Exception($"Can't get Rating, Error{ex.Message}", ex);
            }
        }
        public async Task Rate(AddRatingDTO ratingDto)
        {
            if (ratingDto == null)
            {
                throw new ArgumentNullException("rating not found");
            }
            try
            {
                await _ratingRepository.Rate(ratingDto.AddRatingToEntity());
            }
            catch (Exception ex)
            {

                throw new Exception($"can't rate, Error: {ex.Message}", ex);
            }
        }

        public async Task Update(UpdateRatingDTO rating)
        {
            if (rating == null)
            {
                throw new ArgumentNullException("There is no rating to update");
            }
            try
            {
                await _ratingRepository.UpdateRate(rating.Id, rating.AvgService, rating.AvgWait, rating.Act);
            }
            catch (Exception ex)
            {

                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }
        public async Task Delete(int rateId)
        {
            try
            {
                await _ratingRepository.DeleteRate(rateId);
            }
            catch (Exception ex)
            {

                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }
    }
}
