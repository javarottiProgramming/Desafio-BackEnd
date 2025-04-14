using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IRentalService
    {
        Task<bool> CreateRentalAsync(CreateRentalModel rental);

        Task<RentalDto> GetRentalByIdAsync(string id);

        Task<bool> CreateRentalReturnByIdAsync(string id, RentalReturnDto rentalReturnDate);
    }
}