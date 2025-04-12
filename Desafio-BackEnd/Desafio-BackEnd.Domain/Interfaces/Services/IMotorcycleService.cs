using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IMotorcycleService
    {
        Task<MotorcycleRequest> GetMotorcycleByIdAsync(string id);
        Task<MotorcycleRequest> GetMotorcycleByPlateAsync(string plate);
        Task<bool> CreateMotorcycleAsync(MotorcycleRequest motorcycle);
        Task<bool> UpdateMotorcycleAsync(string id, MotorcycleUpdate motorcycle);
        Task<bool> DeleteMotorcycleAsync(string id);
    }
}