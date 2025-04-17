using Desafio_BackEnd.Domain.Dtos;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IDeliveryManService
    {
        Task<bool> CreateDeliveryManAsync(DeliveryManDto deliveryMan);

        Task<bool> UploadDocumentImageAsync(string id, string documentImgBase64);
    }
}