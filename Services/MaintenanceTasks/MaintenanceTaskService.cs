using Microsoft.EntityFrameworkCore;
using Repository;
using AutoMapper;
using DbContext = Repository.DbContext;

namespace Services.MaintenanceTasks
{
    public class MaintenanceTaskService : IMaintenanceTaskService
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public MaintenanceTaskService(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetMaintenanceTaskDto>> GetAllAsync()
        {
            try
            {
                var tasks = await _context.EquipmentMaintenances
                    .Include(em => em.MaintenanceTask)
                    .Include(em => em.Equipment)
                        .ThenInclude(e => e.EquipmentType)
                    .ToListAsync();
                return _mapper.Map<List<GetMaintenanceTaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to retrieve maintenance tasks list.", ex);
            }
        }

        public async Task<GetMaintenanceTaskDto?> GetByIdAsync(int id)
        {
            var task = await _context.EquipmentMaintenances
                .Include(em => em.MaintenanceTask)
                .Include(em => em.Equipment)
                    .ThenInclude(e => e.EquipmentType)
                .FirstOrDefaultAsync(em => em.MaintenanceTaskId == id);
            if (task == null)
                throw new GlobalExceptionHandler($"MaintenanceTask with id {id} not found.");
            return _mapper.Map<GetMaintenanceTaskDto>(task);
        }

        public async Task<GetMaintenanceTaskDto> CreateAsync(UpSertMaintenanceTaskDto dto)
        {
            try
            {
                var task = _mapper.Map<MaintenanceTask>(dto);
                _context.MaintenanceTasks.Add(task);
                await _context.SaveChangesAsync();
                // Add EquipmentMaintenance link
                var equipmentMaintenance = new EquipmentMaintenance
                {
                    EquipmentId = dto.EquipmentId,
                    MaintenanceTaskId = task.Id
                };
                _context.EquipmentMaintenances.Add(equipmentMaintenance);
                await _context.SaveChangesAsync();
                return _mapper.Map<GetMaintenanceTaskDto>(task);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to create maintenance task.", ex);
            }
        }

        public async Task<GetMaintenanceTaskDto> UpdateAsync(int id, UpSertMaintenanceTaskDto dto)
        {
            var taskToUpdate = await _context.MaintenanceTasks.FindAsync(id);
            if (taskToUpdate == null)
                throw new GlobalExceptionHandler($"MaintenanceTask with id {id} not found.");
            _mapper.Map(dto, taskToUpdate);
            try
            {
                await _context.SaveChangesAsync();
                // Update EquipmentMaintenance link if needed
                var equipmentMaintenance = await _context.EquipmentMaintenances.FirstOrDefaultAsync(em => em.MaintenanceTaskId == id);
                if (equipmentMaintenance != null)
                {
                    if (equipmentMaintenance.EquipmentId != dto.EquipmentId)
                    {
                        _context.EquipmentMaintenances.Remove(equipmentMaintenance);
                        await _context.SaveChangesAsync();

                        var newEquipmentMaintenance = new EquipmentMaintenance
                        {
                            EquipmentId = dto.EquipmentId,
                            MaintenanceTaskId = id
                        };
                        _context.EquipmentMaintenances.Add(newEquipmentMaintenance);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    var newEquipmentMaintenance = new EquipmentMaintenance
                    {
                        EquipmentId = dto.EquipmentId,
                        MaintenanceTaskId = id
                    };
                    _context.EquipmentMaintenances.Add(newEquipmentMaintenance);
                    await _context.SaveChangesAsync();
                }
                return _mapper.Map<GetMaintenanceTaskDto>(taskToUpdate);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to update maintenance task.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.MaintenanceTasks.FindAsync(id);
            if (task == null)
                throw new GlobalExceptionHandler($"MaintenanceTask with id {id} not found.");
            _context.MaintenanceTasks.Remove(task);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to delete maintenance task.", ex);
            }
        }
    }
} 