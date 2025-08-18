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
	}
}
