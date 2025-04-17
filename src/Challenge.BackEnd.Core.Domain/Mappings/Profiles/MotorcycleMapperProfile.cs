using AutoMapper;
using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Entities;
using Challenge.BackEnd.Core.Domain.Events;

namespace Challenge.BackEnd.Core.Domain.Mappings.Profiles
{
    public class MotorcycleMapperProfile : Profile
    {
        public MotorcycleMapperProfile()
        {
            CreateMap<Motorcycle, MotorcycleDto>().ReverseMap();
            
            CreateMap<MotorcycleCreatedEvent, MotorcycleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.FabricationYear, opt => opt.MapFrom(src => src.FabricationYear))
                .ReverseMap();
        }
    }
}