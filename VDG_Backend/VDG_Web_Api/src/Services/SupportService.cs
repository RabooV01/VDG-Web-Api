using VDG_Web_Api.src.DTOs;
using VDG_Web_Api.src.Helpers.Pagination;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _repository;
        private readonly IClaimService _claimService;

        public SupportService(IClaimService claimService, ISupportRepository repository)
        {
            _claimService = claimService;
            _repository = repository;
        }

        public async Task AddMessage(string message)
        {
            try
            {
                var userId = _claimService.GetCurrentUserId();
                await _repository.AddMessage(new Models.SupportModel()
                {
                    Message = message,
                    UserId = userId,
                    SentAt = DateTime.Now,
                    Reviewed = false
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteMessage(int messageId)
        {
            try
            {
                await _repository.DeleteMessage(messageId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaginationModel<SupportDto>> GetSupportMessages(int page, int pageSize, string name)
        {
            try
            {
                var messages = await _repository.GetSupportMessages(page, pageSize, name);
                var rows = await _repository.GetTotalRows(s => (s.User.Person.FirstName + " " + s.User.Person.LastName).Contains(name));

                return new PaginationModel<SupportDto>(messages.Select(s => new SupportDto()
                {
                    Id = s.Id,
                    IsReviewed = s.Reviewed,
                    SentAt = s.SentAt,
                    Role = s.User.Role,
                    UserFullName = (s.User.Person.FirstName + " " + s.User.Person.LastName),
                    UserId = s.UserId
                }), (int)Math.Ceiling(((double)rows) / pageSize), page, rows);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ReviewMessage(int messageId)
        {
            try
            {
                await _repository.ReviewMessage(messageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
