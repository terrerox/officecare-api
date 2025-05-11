using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.MaintenanceTasks;
public interface IMaintenanceTaskService
{
    Task<List<GetMaintenanceTaskDto>> GetAllAsync();
    Task<GetMaintenanceTaskDto?> GetByIdAsync(int id);
    Task<GetMaintenanceTaskDto> CreateAsync(UpSertMaintenanceTaskDto dto);
    Task<GetMaintenanceTaskDto> UpdateAsync(int id, UpSertMaintenanceTaskDto dto);
    Task<bool> DeleteAsync(int id);
} 