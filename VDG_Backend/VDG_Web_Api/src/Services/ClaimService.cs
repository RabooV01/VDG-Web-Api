using System.Security.Claims;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
	public class ClaimService : IClaimService
	{
		private readonly ClaimsPrincipal _principal;
		public ClaimService(IHttpContextAccessor httpContextAccessor)
		{
			_principal = httpContextAccessor.HttpContext!.User;
		}
		public int GetCurrentUserId()
		{
			return int.Parse(_principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
		}

		public UserRole GetCurrentUserRole()
		{
			return Enum.Parse<UserRole>(_principal.FindFirstValue(ClaimTypes.Role)!);
		}
	}
}
