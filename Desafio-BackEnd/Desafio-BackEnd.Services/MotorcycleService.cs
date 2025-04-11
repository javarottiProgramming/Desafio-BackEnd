using Desafio_BackEnd.Domain.Interfaces;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        public Task<bool> CreateMotorcycleAsync(Motorcycle motorcycle)
        {
            return Task.FromResult(true);
        }

        public Task<bool> DeleteMotorcycleAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Motorcycle> GetMotorcycleByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMotorcycleAsync(string id, Motorcycle motorcycle)
        {
            throw new NotImplementedException();
        }
    }
}
