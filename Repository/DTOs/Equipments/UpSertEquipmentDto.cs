using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class UpSertEquipmentDto
    {
        [Required]
        public string Brand { get; set; } 
        [Required]
        public string Model { get; set; }
        [Required]
        public int EquipmentTypeId { get; set; }
        [Required]
        public DateOnly PurchaseDate { get; set; }
        public string? SerialNumber { get; set; }
    }
} 