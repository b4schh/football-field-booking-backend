using FootballField.API.Dtos;
using FootballField.API.Dtos.Notification;
using FootballField.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballField.API.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get all notifications
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(ApiResponse<IEnumerable<NotificationDto>>.Ok(notifications, "Lấy danh sách thông báo thành công"));
        }

        /// <summary>
        /// Get notification by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
                return Ok(ApiResponse<string>.Fail("Không tìm thấy thông báo", 404));

            return Ok(ApiResponse<NotificationDto>.Ok(notification, "Lấy thông tin thông báo thành công"));
        }

        /// <summary>
        /// Get notifications by user ID
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(ApiResponse<IEnumerable<NotificationDto>>.Ok(notifications, "Lấy danh sách thông báo thành công"));
        }

        /// <summary>
        /// Get unread notifications by user ID
        /// </summary>
        [HttpGet("user/{userId}/unread")]
        public async Task<IActionResult> GetUnreadByUserId(int userId)
        {
            var notifications = await _notificationService.GetUnreadNotificationsByUserIdAsync(userId);
            return Ok(ApiResponse<IEnumerable<NotificationDto>>.Ok(notifications, "Lấy danh sách thông báo chưa đọc thành công"));
        }

        /// <summary>
        /// Create new notification
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDto createNotificationDto)
        {
            var created = await _notificationService.CreateNotificationAsync(createNotificationDto);
            return Ok(ApiResponse<NotificationDto>.Ok(created, "Tạo thông báo thành công", 201));
        }

        /// <summary>
        /// Mark notification as read
        /// </summary>
        [HttpPost("{id}/mark-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return Ok(ApiResponse<string>.Ok(null, "Đánh dấu đã đọc thành công"));
        }

        /// <summary>
        /// Mark all notifications as read for a user
        /// </summary>
        [HttpPost("user/{userId}/mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead(int userId)
        {
            await _notificationService.MarkAllAsReadAsync(userId);
            return Ok(ApiResponse<string>.Ok(null, "Đánh dấu tất cả đã đọc thành công"));
        }
    }
}
