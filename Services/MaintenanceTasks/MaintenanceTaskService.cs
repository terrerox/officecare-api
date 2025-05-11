using Microsoft.EntityFrameworkCore;
using Repository;
using AutoMapper;
using DbContext = Repository.DbContext;

namespace Services.MaintenanceTasks
{
    public class MaintenanceTaskService : IMaintenanceTaskService
    {
        private readonly IMaintenanceTaskRepository _maintenanceTaskRepository;
        private readonly IEquipmentMaintenanceRepository _equipmentMaintenanceRepository;
        private readonly IMapper _mapper;

        public MaintenanceTaskService(IMaintenanceTaskRepository maintenanceTaskRepository, IEquipmentMaintenanceRepository equipmentMaintenanceRepository, IMapper mapper)
        {
            _maintenanceTaskRepository = maintenanceTaskRepository;
            _equipmentMaintenanceRepository = equipmentMaintenanceRepository;
            _mapper = mapper;
        }

        public async Task<List<GetMaintenanceTaskDto>> GetAllAsync()
        {
            try
            {
                var tasks = await _equipmentMaintenanceRepository.GetAllAsync();
                return _mapper.Map<List<GetMaintenanceTaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to retrieve maintenance tasks list.", ex);
            }
        }

        public async Task<GetMaintenanceTaskDto?> GetByIdAsync(int id)
        {
            var task = await _equipmentMaintenanceRepository.GetByIdAsync(id);
            if (task == null)
                throw new GlobalExceptionHandler($"MaintenanceTask with id {id} not found.");
            return _mapper.Map<GetMaintenanceTaskDto>(task);
        }

        public async Task<GetMaintenanceTaskDto> CreateAsync(UpSertMaintenanceTaskDto dto)
        {
            try
            {
                var task = _mapper.Map<MaintenanceTask>(dto);
                var createdTask = await _maintenanceTaskRepository.CreateAsync(task);
                var equipmentMaintenance = new EquipmentMaintenance
                {
                    EquipmentId = dto.EquipmentId,
                    MaintenanceTaskId = createdTask.Id
                };
                await _equipmentMaintenanceRepository.CreateAsync(equipmentMaintenance);
                return _mapper.Map<GetMaintenanceTaskDto>(createdTask);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to create maintenance task.", ex);
            }
        }

        public async Task<GetMaintenanceTaskDto> UpdateAsync(int id, UpSertMaintenanceTaskDto dto)
        {
            var taskToUpdate = await _maintenanceTaskRepository.GetByIdAsync(id);
            if (taskToUpdate == null)
                throw new GlobalExceptionHandler($"MaintenanceTask with id {id} not found.");
            _mapper.Map(dto, taskToUpdate);
            try
            {
                await _maintenanceTaskRepository.UpdateAsync(taskToUpdate);
                var equipmentMaintenance = (await _equipmentMaintenanceRepository.GetAllAsync()).FirstOrDefault(em => em.MaintenanceTaskId == id);
                if (equipmentMaintenance != null && dto.EquipmentId != equipmentMaintenance.EquipmentId)
                {
                    await _equipmentMaintenanceRepository.DeleteAsync(equipmentMaintenance);

                    var newEquipmentMaintenance = new EquipmentMaintenance
                    {
                        EquipmentId = dto.EquipmentId,
                        MaintenanceTaskId = id
                    };
                    await _equipmentMaintenanceRepository.CreateAsync(newEquipmentMaintenance);
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
            var task = await _maintenanceTaskRepository.GetByIdAsync(id);
            if (task == null)
                throw new GlobalExceptionHandler($"MaintenanceTask with id {id} not found.");
            await _maintenanceTaskRepository.DeleteAsync(task);
            return true;
        }
    }
}