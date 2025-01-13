public class Shipment : BaseEntity
{
    public string order_date { get; set; } // Blijft als tekst
    public string request_date { get; set; } // Blijft als tekst
    public string shipment_date { get; set; } // Blijft als tekst
    public string shipment_type { get; set; }
    public string shipment_status { get; set; }
    public string notes { get; set; }
    public string carrier_code { get; set; }
    public string carrier_description { get; set; }
    public string service_code { get; set; }
    public string payment_type { get; set; }
    public string transfer_mode { get; set; }
    public int total_package_count { get; set; }
    public decimal total_package_weight { get; set; }
    public List<ShipmentItem> items { get; set; }
}
