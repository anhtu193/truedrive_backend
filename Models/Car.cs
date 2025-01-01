namespace truedrive_backend.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int CatalogId { get; set; } 
        public int MakeId { get; set; }
        public string Model { get; set; }
        public string Color {get; set; }
        public string Fuel { get; set; }
        public string Transmission { get; set; }
        public string DriveType { get; set; }
        public int Capacity { get; set; }
        public decimal EngineSize { get; set; }
        public int NumberOfDoors { get; set; }
        public string VinNumber { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
