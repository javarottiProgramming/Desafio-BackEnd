using Challenge.BackEnd.Core.Domain.Entities;

namespace Challenge.BackEnd.Core.Domain.Interfaces.Repositories
{
    public interface IDeliveryManRepository
    {
        Task<bool> CreateDeliveryManAsync(DeliveryMan deliveryMan);
        Task<DeliveryMan?> GetDeliveryManByIdAsync(string id);
    }
}