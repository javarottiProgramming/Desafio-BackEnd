using AutoMapper;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Mappings.Profiles
{
    public class RentalMapperProfile : Profile
    {
        public RentalMapperProfile()
        {
            CreateMap<Rental, RentalDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => "locacao" + src.Id))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id.Replace("locacao", ""))));
            
            CreateMap<CreateRentalModel, Rental>()
                .ForMember(dest => dest.DeliveryManId, opt => opt.MapFrom(src => src.DeliveryManId)) // Mapear DeliveryManId
                .ForMember(dest => dest.MotorcycleId, opt => opt.MapFrom(src => src.MotorcycleId)) // Mapear MotorcycleId
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate)) // Mapear StartDate
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate)) // Mapear EndDate
                .ForMember(dest => dest.ExpectedEndDate, opt => opt.MapFrom(src => src.ExpectedEndDate)) // Mapear ExpectedEndDate
                .ForMember(dest => dest.DailyValue, opt => opt.MapFrom(src => src.DailyValue)) // Mapear DailyValue
                .ForMember(dest => dest.Plan, opt => opt.MapFrom(src => src.Plan)); // Mapear Plan
        }
    }
}