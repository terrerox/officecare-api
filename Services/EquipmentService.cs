using Microsoft.EntityFrameworkCore;
using Repository;
using AutoMapper;
using System;

namespace Services
{
    public class EquipmentService
    {
        private readonly EquipmentDbContext _context;
        private readonly IMapper _mapper;

        public EquipmentService(EquipmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetEquipmentDto>> GetAllAsync()
        {
            try
            {
                var equipments = await _context.Equipments.Include(e => e.EquipmentType).ToListAsync();
                return _mapper.Map<List<GetEquipmentDto>>(equipments);
            }
            catch (Exception ex)
            {
                throw new EquipmentServiceException("Failed to retrieve equipment list.", ex);
            }
        }

        public async Task<GetEquipmentDto?> GetByIdAsync(int id)
        {
            var equipment = await _context.Equipments.Include(e => e.EquipmentType).FirstOrDefaultAsync(e => e.Id == id);
            if (equipment == null)
                throw new EquipmentServiceException($"Equipment with id {id} not found.");
            return _mapper.Map<GetEquipmentDto>(equipment);
        }
        public async Task<GetEquipmentDto> CreateAsync(UpSertEquipmentDto equipmentDto)
        {
            try
            {
                var equipment = _mapper.Map<Equipment>(equipmentDto);
                _context.Equipments.Add(equipment);
                await _context.SaveChangesAsync();
                var created = await _context.Equipments.Include(e => e.EquipmentType).FirstOrDefaultAsync(e => e.Id == equipment.Id);
                return _mapper.Map<GetEquipmentDto>(created);
            }
            catch (Exception ex)
            {
                throw new EquipmentServiceException("Failed to create equipment.", ex);
            }
        }

        public async Task<GetEquipmentDto> UpdateAsync(int id, UpSertEquipmentDto equipment)
        {
            var equipmentToUpdate = await _context.Equipments.FindAsync(id);
            if (equipmentToUpdate == null)
                throw new EquipmentServiceException($"Equipment with id {id} not found.");
            _mapper.Map(equipment, equipmentToUpdate);
            try
            {
                await _context.SaveChangesAsync();
                var updated = await _context.Equipments.Include(e => e.EquipmentType).FirstOrDefaultAsync(e => e.Id == id);
                return _mapper.Map<GetEquipmentDto>(updated);
            }
            catch (Exception ex)
            {
                throw new EquipmentServiceException("Failed to update equipment.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
                throw new EquipmentServiceException($"Equipment with id {id} not found.");
            _context.Equipments.Remove(equipment);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new EquipmentServiceException("Failed to delete equipment.", ex);
            }
        }
    }
}