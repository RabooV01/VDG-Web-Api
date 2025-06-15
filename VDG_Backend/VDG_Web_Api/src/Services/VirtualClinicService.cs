using VDG_Web_Api.src.DTOs.VirtualClinicDTOs;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services;

public class VirtualClinicService : IVirtualClinicService
{
	public VirtualClinicService() { }
	public Task<VirtualClinicDTO> GetClinicById(int clinicId)
	{
		return Task.FromResult(new VirtualClinicDTO()
		{
			Id = clinicId,
			Doctor = null,
			DoctorId = null
		});
	}
}
