namespace CartridgeWebApp.Models
{
    public class Cartridge
    {
        public int Id { get; set; }
        public string CartridgeModel { get; set; } = string.Empty;
        public int PrinterId { get; set; }
        public int TotalQuantity { get; set; }
        public int BookedQuantity { get; set; }
        public int AvailableQuantity => TotalQuantity - BookedQuantity;
    }
}