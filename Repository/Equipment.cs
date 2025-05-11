using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository;

public class Equipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Brand { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Model { get; set; } = null!;

    [Required]
    public int EquipmentTypeId { get; set; }

    [ForeignKey("EquipmentTypeId")]
    public EquipmentType EquipmentType { get; set; } = null!;

    [Required]
    public DateTime PurchaseDate { get; set; }

    [MaxLength(100)]
    public string? SerialNumber { get; set; }

    public ICollection<EquipmentMaintenance> EquipmentMaintenances { get; set; } = new List<EquipmentMaintenance>();
} 