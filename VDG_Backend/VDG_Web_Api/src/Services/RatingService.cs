using VDG_Web_Api.src.DTOs.RatingDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
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

        public async Task<RatingDTO> Get(int id)
        {
            try
            {
                Rating rate = await _ratingRepository.GetRate(id);

                return rate.ToDto();
            }
            catch (Exception ex)
            {

                throw new Exception($"Can't get Rating, Error{ex.Message}", ex);
            }
        }
        public async Task Rate(RatingDTO ratingDto)
        {
            if (ratingDto == null)
            {
                throw new ArgumentNullException("rating not found");
            }
            try
            {
                await _ratingRepository.Rate(ratingDto.ToEntity());
            }
            catch (Exception ex)
            {

                throw new Exception($"can't rate, Error: {ex.Message}", ex);
            }
        }

        public async Task Update(RatingDTO ratingDto)
        {
            if (ratingDto == null)
            {
                throw new ArgumentNullException("There is no rating to update");
            }
            try
            {
                await _ratingRepository.UpdateRate(ratingDto.ToEntity());
            }
            catch (Exception ex)
            {

                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }
        public async Task Delete(RatingDTO ratingDto)
        {
            if (ratingDto == null)
            {
                throw new ArgumentNullException("There is no rating to delete");
            }
            try
            {
                await _ratingRepository.DeleteRate(ratingDto.ToEntity());
            }
            catch (Exception ex)
            {

                throw new Exception($"Something went wrong, Error: {ex.Message}", ex);
            }
        }
    }
}
