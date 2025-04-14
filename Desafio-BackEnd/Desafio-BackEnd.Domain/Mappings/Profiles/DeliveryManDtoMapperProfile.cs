using AutoMapper;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Mappings.Profiles
{
    public class DeliveryManDtoMapperProfile : Profile
    {
        public DeliveryManDtoMapperProfile()
        {
            CreateMap<DeliveryMan, DeliveryManDto>().ReverseMap();
        }
    }
}