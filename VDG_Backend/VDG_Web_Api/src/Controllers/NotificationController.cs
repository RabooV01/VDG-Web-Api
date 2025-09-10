using Microsoft.AspNetCore.Mvc;
using VDG_Web_Api.src.Models;
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
        public async Task<ActionResult<IEnumerable<Notification>>> Get(int adminId)
        {
            if (adminId < 0)
                return BadRequest();
            try
            {
                var notifications = await _notificationService.GetAllNotifications(adminId);
                return notifications.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
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
