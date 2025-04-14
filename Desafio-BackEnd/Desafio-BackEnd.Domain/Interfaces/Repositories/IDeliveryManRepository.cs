using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Interfaces.Repositories
{
    public interface IDeliveryManRepository
    {
        Task<bool> CreateDeliveryManAsync(DeliveryMan deliveryMan);
        Task<DeliveryMan?> GetDeliveryManByIdAsync(string id);
    }
}