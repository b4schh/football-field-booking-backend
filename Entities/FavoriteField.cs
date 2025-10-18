namespace FootballField.API.Entities;

public class FavoriteField
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FieldId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public User User { get; set; } = null!;
    public Field Field { get; set; } = null!;
}
