namespace Services
{
    public class GetEquipmentDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } 
        public string Model { get; set; }
        public int EquipmentTypeId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? SerialNumber { get; set; }
    }
} 