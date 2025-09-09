using System.Linq.Expressions;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
	public interface ISupportRepository
	{
		public Task<int> GetTotalRows(Expression<Func<SupportModel, bool>>? expression);
		public Task<IEnumerable<SupportModel>> GetSupportMessages(int page, int pageSize, string name);
		public Task AddMessage(SupportModel message);
		public Task DeleteMessage(int messageId);
		public Task ReviewMessage(int messageId);

	}
}
