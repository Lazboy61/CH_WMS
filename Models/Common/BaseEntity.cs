using System.Text.Json.Serialization;

public abstract class BaseEntity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt = DateTime.UtcNow;
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt = DateTime.UtcNow;
}
