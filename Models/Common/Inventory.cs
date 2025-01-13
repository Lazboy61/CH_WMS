public class Inventory : BaseEntity
{
    public string item_id { get; set; }
    public string description { get; set; }
    public string item_reference { get; set; }

    // Veel-op-veel-relatie met Location
    public ICollection<Location> locations { get; set; } = new List<Location>();

    public int total_on_hand { get; set; }
    public int total_expected { get; set; }
    public int total_ordered { get; set; }
    public int total_allocated { get; set; }
    public int total_available { get; set; }
}
public class InventoryTemplate : BaseEntity
{
    public string item_id { get; set; }
    public string description { get; set; }
    public string item_reference { get; set; }

    // Alleen locatie-ID's worden hier opgeslagen
    public List<int> locations { get; set; } = new List<int>();

    public int total_on_hand { get; set; }
    public int total_expected { get; set; }
    public int total_ordered { get; set; }
    public int total_allocated { get; set; }
    public int total_available { get; set; }
}
