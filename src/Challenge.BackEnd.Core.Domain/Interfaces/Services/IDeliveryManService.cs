using Challenge.BackEnd.Core.Domain.Dtos;

namespace Challenge.BackEnd.Core.Domain.Interfaces.Services
{
    public interface IDeliveryManService
    {
        Task<bool> CreateDeliveryManAsync(DeliveryManDto deliveryMan);

        Task<bool> UploadDocumentImageAsync(string id, string documentImgBase64);
    }
}