namespace FootballField.API.Entities;

public class SecurityLog
{
    public long Id { get; set; }
    public int? UserId { get; set; }
    public string? Event { get; set; }
    public string? SessionId { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
