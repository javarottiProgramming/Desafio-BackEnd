using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IRentalService
    {
        //CreateRentalAsync
        Task<bool> CreateRentalAsync(CreateRentalModel rental);

        //GetRentalByIdAsync
        Task<RentalDto> GetRentalByIdAsync(string id);

        //SendRentalReturnByIdAsync
        Task<bool> SendRentalReturnByIdAsync(string id, RentalReturnDto rentalReturnDate);

    }
}
