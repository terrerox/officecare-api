using AutoMapper;
using Repository;

namespace Services.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Equipment, GetEquipmentDto>();
            CreateMap<UpSertEquipmentDto, Equipment>();
            CreateMap<MaintenanceTask, GetMaintenanceTaskDto>();
            CreateMap<UpSertMaintenanceTaskDto, MaintenanceTask>();
            CreateMap<EquipmentMaintenance, GetMaintenanceTaskDto>()
                .ConvertUsing(src => new GetMaintenanceTaskDto {
                    Id = src.MaintenanceTask.Id,
                    Description = src.MaintenanceTask.Description
                });
        }
    }
} 