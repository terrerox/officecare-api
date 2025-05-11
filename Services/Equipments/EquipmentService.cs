using Repository;
using AutoMapper;

namespace Services.Equipments
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _repository;
        private readonly IMapper _mapper;

        public EquipmentService(IEquipmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetEquipmentDto>> GetAllAsync()
        {
            try
            {
                var equipments = await _repository.GetAllAsync();
                return _mapper.Map<List<GetEquipmentDto>>(equipments);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to retrieve equipment list.", ex);
            }
        }

        public async Task<GetEquipmentDto?> GetByIdAsync(int id)
        {
            var equipment = await _repository.GetByIdAsync(id);
            if (equipment == null)
                throw new GlobalExceptionHandler($"Equipment with id {id} not found.");
            return _mapper.Map<GetEquipmentDto>(equipment);
        }
        public async Task<GetEquipmentDto> CreateAsync(UpSertEquipmentDto equipmentDto)
        {
            try
            {
                var equipment = _mapper.Map<Equipment>(equipmentDto);
                var created = await _repository.CreateAsync(equipment);
                return _mapper.Map<GetEquipmentDto>(created);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to create equipment.", ex);
            }
        }

        public async Task<GetEquipmentDto> UpdateAsync(int id, UpSertEquipmentDto equipmentDto)
        {
            var equipmentToUpdate = await _repository.GetByIdAsync(id);
            if (equipmentToUpdate == null)
                throw new GlobalExceptionHandler($"Equipment with id {id} not found.");
            _mapper.Map(equipmentDto, equipmentToUpdate);
            try
            {
                var updated = await _repository.UpdateAsync(equipmentToUpdate);
                return _mapper.Map<GetEquipmentDto>(updated);
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to update equipment.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var equipment = await _repository.GetByIdAsync(id);
            if (equipment == null)
                throw new GlobalExceptionHandler($"Equipment with id {id} not found.");
            if (equipment.EquipmentMaintenances != null && equipment.EquipmentMaintenances.Any())
                throw new GlobalExceptionHandler($"Cannot delete equipment with id {id} because it has related maintenance records.");
            try
            {
                await _repository.DeleteAsync(equipment);
                return true;
            }
            catch (Exception ex)
            {
                throw new GlobalExceptionHandler("Failed to delete equipment.", ex);
            }
        }

        public async Task<List<GetMaintenanceTaskDto>> GetMaintenancesByEquipmentIdAsync(int equipmentId)
        {
            try
            {
                var maintenanceTasks = await _repository.GetMaintenancesByEquipmentIdAsync(equipmentId);
                return _mapper.Map<List<GetMaintenanceTaskDto>>(maintenanceTasks);
            }
            catch (KeyNotFoundException ex)
            {
                throw new GlobalExceptionHandler(ex.Message);
            }
        }
    }
}