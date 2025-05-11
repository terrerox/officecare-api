using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository;

public class EquipmentType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = null!;

    public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
} 