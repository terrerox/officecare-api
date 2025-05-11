using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;

namespace Repository
{
    public interface IMaintenanceTaskRepository
    {
        Task<List<MaintenanceTask>> GetAllAsync();
        Task<MaintenanceTask?> GetByIdAsync(int id);
        Task<MaintenanceTask> CreateAsync(MaintenanceTask maintenanceTask);
        Task<MaintenanceTask> UpdateAsync(MaintenanceTask maintenanceTask);
        Task DeleteAsync(MaintenanceTask maintenanceTask);
    }
} 