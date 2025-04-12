using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> GetRentalByIdAsync(string id);
        Task AddRentalAsync(Rental rental);
    }
}
