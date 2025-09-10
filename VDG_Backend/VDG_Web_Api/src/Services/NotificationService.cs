using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;
using VDG_Web_Api.src.Services.Interfaces;

namespace VDG_Web_Api.src.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task DeleteNotification(int notificationId)
        {
            try
            {
                await _notificationRepository.DeleteNotification(notificationId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications(int adminId)
        {
            try
            {
                return await _notificationRepository.GetAllNotifications(adminId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendNotification(Notification notification)
        {
            try
            {
                await _notificationRepository.SendNotification(notification);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
