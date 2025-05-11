using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;

namespace Repository
{
    public interface IEquipmentRepository
    {
        Task<List<Equipment>> GetAllAsync();
        Task<Equipment?> GetByIdAsync(int id);
        Task<Equipment> CreateAsync(Equipment equipment);
        Task<Equipment> UpdateAsync(Equipment equipment);
        Task DeleteAsync(Equipment equipment);
        Task<List<MaintenanceTask>> GetMaintenancesByEquipmentIdAsync(int equipmentId);
    }
} 