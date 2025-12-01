namespace CartridgeWebApp.Models
{
    public class Printer
    {
        public int Id { get; set; }
        public string PrinterModel { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
    }
}