using FootballField.API.Dtos.Notification;

namespace FootballField.API.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync();
        Task<NotificationDto?> GetNotificationByIdAsync(int id);
        Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(int userId);
        Task<IEnumerable<NotificationDto>> GetUnreadNotificationsByUserIdAsync(int userId);
        Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto createNotificationDto);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int userId);
    }
}
