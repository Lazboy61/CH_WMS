using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


public class OrderItem
{
    [Key]
    public int id { get; set; }

    [JsonPropertyName("item_id")]
    public string order_item_id { get; set; }

    public int amount { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [JsonIgnore] // Optioneel: voorkomt serialisatie naar JSON
    public Order? Order { get; set; }
}