using Microsoft.EntityFrameworkCore;
using VDG_Web_Api.src.Data;
using VDG_Web_Api.src.Models;
using VDG_Web_Api.src.Repositories.Interfaces;

namespace VDG_Web_Api.src.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly VdgDbDemoContext _context;
        public NotificationRepository(VdgDbDemoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Notification>> GetAllNotifications(int adminId)
        {
            try
            {
                return await _context.Notifications.Where(n => n.AdminId == adminId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while retrieving data, {ex.Message}", ex);
            }

            throw new NotImplementedException();
        }

        public async Task DeleteNotification(int notificationId)
        {
            try
            {
                await _context.Notifications.Where(n => n.Id == notificationId).ExecuteDeleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting data, {ex.Message}", ex);
            }
        }
        public async Task SendNotification(Notification notification)
        {
            try
            {
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while Adding data, {ex.Message}", ex);
            }
        }
    }
}
