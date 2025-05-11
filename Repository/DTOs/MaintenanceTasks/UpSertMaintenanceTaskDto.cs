using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class UpSertMaintenanceTaskDto
    {
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        
        [Required]
        public int EquipmentId { get; set; }
    }
} 