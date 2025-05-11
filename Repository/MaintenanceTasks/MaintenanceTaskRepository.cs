using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class MaintenanceTaskRepository : IMaintenanceTaskRepository
    {
        private readonly DbContext _context;
        public MaintenanceTaskRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<MaintenanceTask>> GetAllAsync()
        {
            return await _context.MaintenanceTasks.Include(mt => mt.EquipmentMaintenances).ToListAsync();
        }

        public async Task<MaintenanceTask?> GetByIdAsync(int id)
        {
            return await _context.MaintenanceTasks.Include(mt => mt.EquipmentMaintenances).FirstOrDefaultAsync(mt => mt.Id == id);
        }

        public async Task<MaintenanceTask> CreateAsync(MaintenanceTask maintenanceTask)
        {
            var result = _context.MaintenanceTasks.Add(maintenanceTask);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<MaintenanceTask> UpdateAsync(MaintenanceTask maintenanceTask)
        {
            var result = _context.MaintenanceTasks.Update(maintenanceTask);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(MaintenanceTask maintenanceTask)
        {
            _context.MaintenanceTasks.Remove(maintenanceTask);
            await _context.SaveChangesAsync();
        }
    }
} 