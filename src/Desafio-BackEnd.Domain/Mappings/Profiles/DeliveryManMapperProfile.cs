using AutoMapper;
using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Entities;

namespace Challenge.BackEnd.Core.Domain.Mappings.Profiles
{
    public class DeliveryManMapperProfile : Profile
    {
        public DeliveryManMapperProfile()
        {
            CreateMap<DeliveryMan, DeliveryManDto>().ReverseMap();
        }
    }
}