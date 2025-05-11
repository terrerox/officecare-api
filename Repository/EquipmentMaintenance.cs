using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository;

public class EquipmentMaintenance
{
    [Key, Column(Order = 0)]
    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;

    [Key, Column(Order = 1)]
    public int MaintenanceTaskId { get; set; }
    public MaintenanceTask MaintenanceTask { get; set; } = null!;
} 