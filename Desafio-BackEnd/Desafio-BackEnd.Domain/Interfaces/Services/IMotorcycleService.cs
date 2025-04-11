using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IMotorcycleService
    {
        Task<Motorcycle> GetMotorcycleByIdAsync(string id);
        Task<Motorcycle> GetMotorcycleByPlateAsync(string plate);
        Task<bool> CreateMotorcycleAsync(Motorcycle motorcycle);
        Task<bool> UpdateMotorcycleAsync(string id, MotorcycleUpdate motorcycle);
        Task<bool> DeleteMotorcycleAsync(string id);
    }
}