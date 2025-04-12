using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IDeliveryMenService
    {
        Task<bool> CreateDeliveryManAsync(DeliveryManRequest deliveryMan);

        Task<bool> SendDocumentImageAsync(string id, string documentImgBase64);
    }
}