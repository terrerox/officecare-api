using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;

namespace Repository
{
    public interface IEquipmentMaintenanceRepository
    {
        Task<List<EquipmentMaintenance>> GetAllAsync();
        Task<EquipmentMaintenance?> GetByIdAsync(int maintenanceTaskId);
        Task<EquipmentMaintenance> CreateAsync(EquipmentMaintenance equipmentMaintenance);
        Task<EquipmentMaintenance> UpdateAsync(EquipmentMaintenance equipmentMaintenance);
        Task DeleteAsync(EquipmentMaintenance equipmentMaintenance);
    }
} 