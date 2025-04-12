using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task AddRentalAsync(Rental rental);
    }
}
