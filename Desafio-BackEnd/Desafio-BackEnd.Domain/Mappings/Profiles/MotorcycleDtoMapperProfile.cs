using AutoMapper;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Mappings.Profiles
{
    public class MotorcycleDtoMapperProfile : Profile
    {
        public MotorcycleDtoMapperProfile()
        {
            CreateMap<Motorcycle, MotorcycleDto>().ReverseMap();
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                //.ForMember(dest => dest.FabricationYear, opt => opt.MapFrom(src => src.FabricationYear))
                //.ForMember(dest => dest.Plate, opt => opt.MapFrom(src => src.Plate));

        }


    }
}