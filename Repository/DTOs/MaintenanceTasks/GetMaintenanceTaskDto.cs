using System.Collections.Generic;

namespace Services
{
    public class GetMaintenanceTaskDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public GetEquipmentDto Equipment { get; set; }
    }
} 