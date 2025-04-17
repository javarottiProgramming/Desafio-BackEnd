using Challenge.BackEnd.Core.Domain.Dtos;

namespace Challenge.BackEnd.Core.Domain.Interfaces.Services
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