using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental?> GetRentalByIdAsync(int id);
        Task<bool> CreateRentalAsync(Rental rental);
        Task<bool> UpdateRentalReturnByIdAsync(Rental rental);
    }
}