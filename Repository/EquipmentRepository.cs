using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DbContext _context;
        public EquipmentRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Equipment>> GetAllAsync()
        {
            return await _context.Equipments.Include(e => e.EquipmentType).ToListAsync();
        }

        public async Task<Equipment?> GetByIdAsync(int id)
        {
            return await _context.Equipments.Include(e => e.EquipmentType).Include(e => e.EquipmentMaintenances).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Equipment> CreateAsync(Equipment equipment)
        {
            var result = _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Equipment> UpdateAsync(Equipment equipment)
        {
            var result = _context.Equipments.Update(equipment);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(Equipment equipment)
        {
            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MaintenanceTask>> GetMaintenancesByEquipmentIdAsync(int equipmentId)
        {
            var equipment = await _context.Equipments
                .Include(e => e.EquipmentMaintenances)
                    .ThenInclude(em => em.MaintenanceTask)
                .FirstOrDefaultAsync(e => e.Id == equipmentId);
            if (equipment == null)
                throw new KeyNotFoundException($"Equipment with id {equipmentId} not found.");
            return equipment.EquipmentMaintenances.Select(em => em.MaintenanceTask).ToList();
        }
    }
} 