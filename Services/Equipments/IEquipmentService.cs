using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Equipments;
public interface IEquipmentService
{
    Task<List<GetEquipmentDto>> GetAllAsync();
    Task<GetEquipmentDto?> GetByIdAsync(int id);
    Task<GetEquipmentDto> CreateAsync(UpSertEquipmentDto equipmentDto);
    Task<GetEquipmentDto> UpdateAsync(int id, UpSertEquipmentDto equipment);
    Task<bool> DeleteAsync(int id);
    Task<List<GetMaintenanceTaskDto>> GetMaintenancesByEquipmentIdAsync(int equipmentId);
} 