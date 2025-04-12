using AutoMapper;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Mappings.Profiles
{
    public class RentalDtoMapperProfile : Profile
    {
        public RentalDtoMapperProfile()
        {
            //TODO Nao precisa dos formembers, pois o AutoMapper faz isso automaticamente

            CreateMap<Rental, RentalDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Converter Id para string
                .ForMember(dest => dest.MotorCycleId, opt => opt.MapFrom(src => src.MotorcycleId)) // Mapear MotorcycleId para MotoId
                .ForMember(dest => dest.DeliveryManId, opt => opt.MapFrom(src => src.DeliveryManId)) // Mapear DeliveryManId
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate)) // Mapear StartDate
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate)) // Mapear EndDate
                .ForMember(dest => dest.ExpectedEndDate, opt => opt.MapFrom(src => src.ExpectedEndDate)) // Mapear ExpectedEndDate
                .ForMember(dest => dest.DailyValue, opt => opt.MapFrom(src => src.DailyValue)); // Mapear DailyValue
        }
    }
}