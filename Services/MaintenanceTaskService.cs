using Microsoft.EntityFrameworkCore;
using Repository;
using AutoMapper;

namespace Services
{
    public class MaintenanceTaskService
    {
        private readonly EquipmentDbContext _context;
        private readonly IMapper _mapper;

        public MaintenanceTaskService(EquipmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetMaintenanceTaskDto>> GetAllAsync()
        {
            try
            {
                var tasks = await _context.MaintenanceTasks.ToListAsync();
                return _mapper.Map<List<GetMaintenanceTaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to retrieve maintenance tasks list.", ex);
            }
        }

        public async Task<GetMaintenanceTaskDto?> GetByIdAsync(int id)
        {
            var task = await _context.MaintenanceTasks.FindAsync(id);
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