namespace FootballField.API.Entities;

public class OwnerSetting
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public decimal? DepositRate { get; set; }
    public string? CancelRefundPolicy { get; set; }
    public int? MinBookingNotice { get; set; }
    public bool AllowChatbot { get; set; } = false;
    public bool AllowReview { get; set; } = true;
    public string? DefaultCurrency { get; set; }
    public decimal? PlatformFeeRate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public User Owner { get; set; } = null!;
}
