using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.DTOs.NotificationDTOs;
using VDG_Web_Api.src.Mapping;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDTO>>> Get(int adminId)
        {
            if (adminId < 0)
                return BadRequest();
            try
            {
                var notifications = await _notificationService.GetAllNotifications(adminId);
                return notifications.Select(notification => notification.ToDto()).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]

        public async Task<ActionResult> Send(NotificationDTO notification)
        {
            if (notification == null)
                return BadRequest();

            try
            {
                await _notificationService.SendNotification(notification.ToEntity());
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int notificationId)
        {
            if (notificationId < 0)
                return BadRequest();
            try
            {
                await _notificationService.DeleteNotification(notificationId);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
