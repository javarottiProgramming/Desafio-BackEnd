using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IMotorcycleService
    {
        Task<Motorcycle> GetMotorcycleByIdAsync(string id);
        Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync();
        Task<bool> CreateMotorcycleAsync(Motorcycle motorcycle);
        Task<bool> UpdateMotorcycleAsync(string id, Motorcycle motorcycle);
        Task<bool> DeleteMotorcycleAsync(string id);
    }
}