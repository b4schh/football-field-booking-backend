using System.ComponentModel.DataAnnotations;
using FootballField.API.Entities;

namespace FootballField.API.Dtos.Notification
{
    public class CreateNotificationDto
    {
        [Required(ErrorMessage = "UserId là bắt buộc")]
        public int UserId { get; set; }

        public int? SenderId { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Nội dung là bắt buộc")]
        public string Message { get; set; } = null!;

        public NotificationType Type { get; set; } = NotificationType.System;

        public string? RelatedTable { get; set; }
        public int? RelatedId { get; set; }
    }
}
