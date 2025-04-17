using AutoMapper;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Events;

namespace Desafio_BackEnd.Domain.Mappings.Profiles
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