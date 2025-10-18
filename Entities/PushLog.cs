namespace FootballField.API.Entities;

// Enums
public enum PushLogStatus : byte
{
    Pending = 0,
    Sent = 1,
    Failed = 2
}

public class PushLog
{
    public long Id { get; set; }
    public int NotificationId { get; set; }
    public string? Channel { get; set; }
    public PushLogStatus Status { get; set; } = PushLogStatus.Pending;
    public string? Response { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public Notification Notification { get; set; } = null!;
}
