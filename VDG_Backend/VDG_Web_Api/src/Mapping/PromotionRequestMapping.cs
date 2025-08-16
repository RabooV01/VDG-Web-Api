using VDG_Web_Api.src.DTOs.PromotionRequest;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping
{
	public static class PromotionRequestMapping
	{
		public static PromotionRequestDto ToDto(this PromotionRequest request)
			=> new()
			{
				AdminId = request.RespondBy,
				ApprovedBy = request.Admin?.Person.FirstName,
				ApprovementDate = request.ResponseDate,
				FullName = $"{request.User.Person.FirstName} {request.User.Person.LastName ?? string.Empty}",
				Note = request.Note,
				Personal_Id = request.User.Person.PersonalId!,
				SyndicateId = request.SyndicateId,
				UserId = request.UserId,
				Id = request.Id
			};

		public static PromotionRequest ToEntity(this AddPromotionRequest request, int userId)
			=> new()
			{
				RequestedAt = DateTime.Now,
				Note = request.Note,
				SyndicateId = request.SyndicateId,
				UserId = userId,
				SpecialityId = request.specialityId
			};
	}
}
