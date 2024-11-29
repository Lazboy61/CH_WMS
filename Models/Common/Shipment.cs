using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Shipment : BaseEntity
{
    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }

    [JsonPropertyName("source_id")]
    public int SourceId { get; set; } // deze mocht weg denk ik want het wees verder nergens naar.

    [JsonPropertyName("order_date")]
    public DateOnly OrderDate { get; set; } // dateonly??? want ->  "order_date": "2024-05-01",  -> in python versie

    [JsonPropertyName("request_date")]
    public DateOnly RequestDate { get; set; } // dateonly??? ...

    [JsonPropertyName("shipment_date")]
    public DateOnly ShipmentDate { get; set; } // dateonly??? ...

    [JsonPropertyName("shipment_type")]
    public string ShipmentType { get; set; }

    [JsonPropertyName("shipment_status")]
    public string ShipmentStatus { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("carrier_code")]
    public string CarrierCode { get; set; }

    [JsonPropertyName("carrier_description")]
    public string CarrierDescription { get; set; }

    [JsonPropertyName("service_code")]
    public string ServiceCode { get; set; }

    [JsonPropertyName("payment_type")]
    public string PaymentType { get; set; }

    [JsonPropertyName("transfer_mode")]
    public string TransferMode { get; set; }

    [JsonPropertyName("total_package_count")]
    public int TotalPackageCount { get; set; }

    [JsonPropertyName("total_package_weight")]
    public decimal TotalPackageWeight { get; set; }

    [JsonPropertyName("items")]
    public List<ShipmentItem> Items { get; set; }
}
