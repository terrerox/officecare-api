using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository;

public class MaintenanceTask
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Description { get; set; } = null!;

    public ICollection<EquipmentMaintenance> EquipmentMaintenances { get; set; } = new List<EquipmentMaintenance>();
} 