using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        public Task SendNotification(Notification notification);
        public Task<IEnumerable<Notification>> GetAllNotifications(int adminId);
        public Task DeleteNotification(int notificationId);
    }
}
