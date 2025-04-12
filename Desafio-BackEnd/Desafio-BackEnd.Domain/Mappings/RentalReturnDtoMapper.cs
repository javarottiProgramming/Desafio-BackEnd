using AutoMapper;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Mappings
{
    public class RentalReturnDtoMapperProfile : Profile
    {
        public RentalReturnDtoMapperProfile()
        {
            CreateMap<Rental, RentalReturnDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Converter Id para string
                .ForMember(dest => dest.MotorCycleId, opt => opt.MapFrom(src => src.MotorcycleId)) // Mapear MotorcycleId para MotoId
                .ForMember(dest => dest.DeliveryManId, opt => opt.MapFrom(src => src.DeliveryManId)) // Mapear DeliveryManId
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate)) // Mapear StartDate
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate)) // Mapear EndDate
                .ForMember(dest => dest.ExpectedEndDate, opt => opt.MapFrom(src => src.ExpectedEndDate)) // Mapear ExpectedEndDate
                .ForMember(dest => dest.DailyValue, opt => opt.MapFrom(src => src.DailyValue)); // Mapear DailyValue

            //CreateMap<RentalReturn, Rental>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id)))
            //    .ForMember(dest => dest.MotorcycleId, opt => opt.MapFrom(src => src.MotoId))
            //    .ForMember(dest => dest.StartDate, opt => opt.Ignore()) // Ignorar StartDate, pois não está presente em RentalReturn
            //    .ForMember(dest => dest.EndDate, opt => opt.Ignore()) // Ignorar EndDate, pois não está presente em RentalReturn
            //    .ForMember(dest => dest.ExpectedEndDate, opt => opt.Ignore()) // Ignorar ExpectedEndDate, pois não está presente em RentalReturn
            //    .ForMember(dest => dest.Plan, opt => opt.Ignore()) // Ignorar Plan, pois não está presente em RentalReturn
            //    .ForMember(dest => dest.DailyValue, opt => opt.Ignore()) // Ignorar DailyValue, pois não está presente em RentalReturn
            //    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Ignorar CreatedDate, pois não está presente em RentalReturn
            //    .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore()); // Ignorar UpdatedDate, pois não está presente em RentalReturn

        }
    }
}
