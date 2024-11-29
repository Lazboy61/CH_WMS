using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Transfer : BaseEntity
{
    [JsonPropertyName("reference")]
    public string Reference { get; set; }

    [JsonPropertyName("transfer_from")]
    public int TransferFrom { get; set; } // ID? // tussen inventories? denk dat als we verder zijn dit mss wel achterkomen

    [JsonPropertyName("transfer_to")]
    public int TransferTo { get; set; }  // ID??

    [JsonPropertyName("transfer_status")]
    public string TransferStatus { get; set; }

    [JsonPropertyName("items")]
    public List<TransferItem> Items { get; set; }
}
