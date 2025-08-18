using VDG_Web_Api.src.DTOs.PromotionRequest;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
	public class PromotionRequestService : IPromotionRequestService
	{
		private readonly IPromotionRequestRepository _promotionRequestRepository;
		private readonly IUserRepository _userRepository;
		private readonly IDoctorRepository _doctorRepository;
		private readonly IClaimService _claimService;
		public PromotionRequestService(IPromotionRequestRepository promotionRequestRepository, IClaimService claimService, IUserRepository userRepository, IDoctorRepository doctorRepository)
		{
			_promotionRequestRepository = promotionRequestRepository;
			_claimService = claimService;
			_userRepository = userRepository;
			_doctorRepository = doctorRepository;
		}

		public async Task ApproveRequest(int requestId)
		{
			try
			{
				var request = await _promotionRequestRepository.GetById(requestId);

				if (request == null)
				{
					throw new ArgumentNullException(nameof(requestId), "Invalid request Id");
				}

				request.ResponseDate = DateTime.Now;
				request.RespondBy = _claimService.GetCurrentUserId();
				request.Status = PromotionStatus.Approved;

				await _promotionRequestRepository.Update(request);

				User user = (await _userRepository.GetById(request.UserId))!;
				user.Role = UserRole.Doctor;

				await _userRepository.UpdateUserAsync(user);

				Doctor doctor = new()
				{
					Description = "",
					SpecialityId = request.SpecialityId,
					UserId = request.UserId,
					SyndicateId = request.SyndicateId
				};

				await _doctorRepository.AddDoctorAsync(doctor);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task DeleteRequest(int requestId)
		{
			try
			{
				await _promotionRequestRepository.Delete(requestId);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<IEnumerable<PromotionRequestDto>> GetRequests(int page, int pageSize, bool onlyPending, string? name)
		{
			try
			{
				var requests = await _promotionRequestRepository.GetPromotionRequests(page, pageSize, onlyPending, name);
				return requests.Select(r => r.ToDto());
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task RejectRequest(int requestId)
		{
			try
			{
				var request = await _promotionRequestRepository.GetById(requestId);

				if (request == null)
				{
					throw new ArgumentNullException(nameof(requestId), "Invalid request Id");
				}

				request.ResponseDate = DateTime.Now;
				request.RespondBy = _claimService.GetCurrentUserId();
				request.Status = PromotionStatus.Rejected;

				await _promotionRequestRepository.Update(request);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task RequestPromotion(AddPromotionRequest request)
		{
			try
			{
				await _promotionRequestRepository.RequestPromotion(request.ToEntity(_claimService.GetCurrentUserId()));
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
