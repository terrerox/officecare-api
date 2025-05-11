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
        }
    }
} 