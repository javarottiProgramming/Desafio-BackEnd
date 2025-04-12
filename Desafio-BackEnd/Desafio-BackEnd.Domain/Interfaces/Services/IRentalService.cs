using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Domain.Interfaces.Services
{
    public interface IRentalService
    {
        //CreateRentalAsync
        Task<bool> CreateRentalAsync(RentalDto rental);

        //GetRentalByIdAsync
        Task<RentalReturnDto> GetRentalByIdAsync(string id);

        //SendRentalReturnByIdAsync
        Task<bool> SendRentalReturnByIdAsync(string id, RentalReturnDate rentalReturnDate);

    }
}
