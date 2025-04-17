using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Interfaces.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<bool> CreateMotorcycleAsync(Motorcycle motorcycle);
        Task<Motorcycle?> GetMotorcycleByPlateAsync(string plate);
        Task<Motorcycle?> GetMotorcycleByIdAsync(string id);
        Task<bool> UpdateMotorcyclePlateByIdAsync(string id, string plate);
        Task<bool> DeleteMotorcycleByIdAsync(string id);
    }
}
