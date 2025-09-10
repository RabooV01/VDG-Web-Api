using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VDG_Web_Api.src.DTOs.DoctorDTOs;
using VDG_Web_Api.src.DTOs.FilterDTOs;
using VDG_Web_Api.src.Enums;
using VDG_Web_Api.src.Helpers.Pagination;
using VDG_Web_Api.src.Services.Interfaces;
using VDG_Web_Api.src.Services.SearchService;

namespace VDG_Web_Api.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IClaimService _claimService;
        private readonly ISearchingService _searchingService;
        public DoctorController(IDoctorService doctorService, IClaimService claimService, ISearchingService searchingService)
        {
            _doctorService = doctorService;
            _claimService = claimService;
            _searchingService = searchingService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<DoctorSearchDto>>> GetByName([Required] string name, int page = 1, int pageSize = 20)
        {
            try
            {
                var doctors = await _searchingService.GetByName(name, page, pageSize);
                return Ok(doctors);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<PaginationModel<DoctorSearchDto>>> GetFilteredDoctors(string? name, [Required] int SpecialtyId, string? gender, double? cost, double? minRate, double? lat, double? lon, bool ShortestDistanceFirst = false, int page = 1, int pageSize = 20)
        {
            try
            {
                if (ShortestDistanceFirst && (lon is null || lat is null))
                {
                    return BadRequest("Must access location to get clinics by shortest distance");
                }

                FilterDTO filter = new()
                {
                    Gender = gender,
                    MinRate = minRate,
                    ShortestDistanceFirst = ShortestDistanceFirst,
                    SpecialityId = SpecialtyId,
                    UserLat = lat,
                    UserLon = lon
                };
                if (cost is not null)
                {
                    filter.CostRange = cost.Value;
                }
                var doctors = await _searchingService.SearchDoctorAsync(filter, page, pageSize);

                return Ok(doctors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        //[Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAll(int page = 1, int pageSize = 20, int? specialityId = null, string? name = null)
        {
            try
            {
                var doctors = await _doctorService.GetAllDoctors(page, pageSize, specialityId, name);
                return Ok(doctors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{doctorId}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctor(int doctorId)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorById(doctorId);

                //if (doctor.UserId != _claimService.GetCurrentUserId() && !_claimService.GetCurrentUserRole().Equals(UserRole.Admin))
                //{
                //	return Unauthorized();
                //}

                return Ok(doctor);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = nameof(UserRole.Doctor))]
        public async Task<ActionResult> UpdateDescription(string description)
        {
            try
            {
                await _doctorService.UpdateDoctorDescription(_claimService.GetCurrentDoctorId(), description);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("Settings")]
        [Authorize(Roles = nameof(UserRole.Doctor))]
        public async Task<ActionResult> UpdateDoctorSettings(DoctorSettings doctorSettings)
        {
            try
            {
                await _doctorService.UpdateDoctorConsultationSettings(doctorSettings, _claimService.GetCurrentDoctorId());
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{doctorId}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult> DeleteDoctor(int doctorId)
        {
            try
            {
                await _doctorService.DeleteDoctor(doctorId);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(UserRole.Admin)}")]
        public async Task<ActionResult> PromoteUserToDoctor(AddDoctorDTO doctorDTO)
        {
            try
            {
                await _doctorService.AddDoctor(doctorDTO);
                return Created();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public async Task<ActionResult<DoctorDTO>> GetTopDoctor(int cnt = 10)
        {
            try
            {
                var topdoctor = await _doctorService.GetTopDoctor(cnt);
                return Ok(topdoctor);
            }
            catch (Exception)
            {
                throw;
            }

        }



    }
}
