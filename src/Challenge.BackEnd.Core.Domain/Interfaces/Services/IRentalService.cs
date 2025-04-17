using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Models;

namespace Challenge.BackEnd.Core.Domain.Interfaces.Services
{
    public interface IRentalService
    {
        Task<bool> CreateRentalAsync(CreateRentalModel rental);

        Task<RentalDto?> GetRentalByIdAsync(string id);

        Task<bool> UpdateRentalReturnByIdAsync(string id, DateTime returnDate);
    }
}