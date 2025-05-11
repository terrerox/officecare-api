using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class EquipmentMaintenanceRepository : IEquipmentMaintenanceRepository
    {
        private readonly DbContext _context;
        public EquipmentMaintenanceRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<EquipmentMaintenance>> GetAllAsync()
        {
            return await _context.EquipmentMaintenances
                .Include(em => em.MaintenanceTask)
                .Include(em => em.Equipment)
                .ThenInclude(mt => mt.EquipmentType)
                .ToListAsync();
        }

        public async Task<EquipmentMaintenance?> GetByIdAsync(int maintenanceTaskId)
        {
            return await _context.EquipmentMaintenances
                .Include(em => em.MaintenanceTask)
                .Include(em => em.Equipment)
                .ThenInclude(mt => mt.EquipmentType)
                .FirstOrDefaultAsync(em => em.MaintenanceTaskId == maintenanceTaskId);
        }

        public async Task<EquipmentMaintenance> CreateAsync(EquipmentMaintenance equipmentMaintenance)
        {
            var result = _context.EquipmentMaintenances.Add(equipmentMaintenance);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<EquipmentMaintenance> UpdateAsync(EquipmentMaintenance equipmentMaintenance)
        {
            var result = _context.EquipmentMaintenances.Update(equipmentMaintenance);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(EquipmentMaintenance equipmentMaintenance)
        {
            _context.EquipmentMaintenances.Remove(equipmentMaintenance);
            await _context.SaveChangesAsync();
        }
    }
} 