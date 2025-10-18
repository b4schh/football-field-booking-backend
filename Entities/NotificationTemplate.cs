namespace FootballField.API.Entities;

public class NotificationTemplate
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? TitleTemplate { get; set; }
    public string? MessageTemplate { get; set; }
    public byte? Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
