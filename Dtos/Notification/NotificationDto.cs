using FootballField.API.Entities;

namespace FootballField.API.Dtos.Notification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? SenderId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public NotificationType Type { get; set; }
        public string? RelatedTable { get; set; }
        public int? RelatedId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
