using Desafio_BackEnd.Domain.Dtos;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IMotorcycleService
    {
        Task<bool> CreateMotorcycleAsync(MotorcycleDto motorcycle);
        Task<MotorcycleDto?> GetMotorcycleByPlateAsync(string plate);
        Task<MotorcycleDto?> GetMotorcycleByIdAsync(string id);
        Task<bool> UpdateMotorcyclePlateByIdAsync(string id, string plate);
        Task<bool> DeleteMotorcycleByIdAsync(string id);
    }
}