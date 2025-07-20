using VDG_Web_Api.src.Enums;

namespace VDG_Web_Api.src.Services.Interfaces
{
	public interface IClaimService
	{
		public int GetCurrentUserId();
		public UserRole GetCurrentUserRole();
	}
}
