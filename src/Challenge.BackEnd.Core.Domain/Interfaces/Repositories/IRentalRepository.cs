using Challenge.BackEnd.Core.Domain.Entities;

namespace Challenge.BackEnd.Core.Domain.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental?> GetRentalByIdAsync(int id);
        Task<bool> CreateRentalAsync(Rental rental);
        Task<bool> UpdateRentalReturnByIdAsync(Rental rental);
    }
}