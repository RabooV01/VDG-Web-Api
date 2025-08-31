using System.Security.Claims;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
	public class ClaimService : IClaimService
	{
		private readonly ClaimsPrincipal _principal;
		private readonly IDoctorRepository _doctorRepository;
		public ClaimService(IHttpContextAccessor httpContextAccessor, IDoctorRepository doctorRepository)
		{
			_principal = httpContextAccessor.HttpContext!.User;
			_doctorRepository = doctorRepository;
		}
		public int GetCurrentUserId()
		{
			return int.Parse(_principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
		}

		public int GetCurrentDoctorId()
		{
			int doctorId = int.Parse(_principal.FindFirstValue("DoctorId")!);
			return doctorId;
		}

		public UserRole GetCurrentUserRole()
		{
			return Enum.Parse<UserRole>(_principal.FindFirstValue(ClaimTypes.Role)!);
		}

		public async Task<bool> IsCurrentDoctor(int userId)
		{
			var doctor = await _doctorRepository.GetDoctorByUserId(userId);
			return doctor?.User.Id == GetCurrentUserId();
		}

		public bool IsAdmin()
		{
			return GetCurrentUserRole().Equals(UserRole.Admin);
		}
	}
}
