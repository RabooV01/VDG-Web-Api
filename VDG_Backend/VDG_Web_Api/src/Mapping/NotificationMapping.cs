using VDG_Web_Api.src.DTOs.NotificationDTOs;
using VDG_Web_Api.src.Models;

namespace VDG_Web_Api.src.Mapping
{
    public static class NotificationMapping
    {
        public static Notification ToEntity(this NotificationDTO NotificationDTO)
            => new()
            {
                AdminId = NotificationDTO.AdminId,
                Date = NotificationDTO.Date,
                Title = NotificationDTO.Title,
                Message = NotificationDTO.Message
            };

        public static NotificationDTO ToDto(this Notification Notification)
            => new()
            {
                AdminId = Notification.AdminId,
                Date = Notification.Date,
                Title = Notification.Title,
                Message = Notification.Message
            };
    }
}
