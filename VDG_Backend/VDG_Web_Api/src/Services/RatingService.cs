using VDG_Web_Api.src.DTOs.RatingDTOs;
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

        public async Task Rate(RatingDTO ratingDto)
        {
            if (ratingDto == null)
            {
                throw new ArgumentNullException("rating not found");
            }
            if (ratingDto.UserId == null || ratingDto.DoctorId == null)
            {
                throw new ArgumentNullException("may there is no a doctor or a user");
            }
            try
            {
                await _ratingRepository.Rate(MapToRating(ratingDto));
            }
            catch (Exception ex)
            {

                throw new Exception($"can't rate, Error: {ex.Message}", ex);
            }
        }

        public Rating MapToRating(RatingDTO ratingDto)
        {
            return new Rating()
            {
                Id = ratingDto.Id,
                UserId = ratingDto.UserId,
                DoctorId = ratingDto.DoctorId,
                Act = ratingDto.Act,
                AvgWait = ratingDto.AvgWait,
                AvgService = ratingDto.AvgService,
            };
        }
    }
}
