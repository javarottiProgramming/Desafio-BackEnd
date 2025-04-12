using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Services
{
    public class RentalService : IRentalService
    {
        public Task<bool> CreateRentalAsync(Rental rental)
        {
            throw new NotImplementedException();
        }
        public Task<RentalReturn> GetRentalByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> SendRentalReturnByIdAsync(string id, RentalReturnDate rentalReturnDate)
        {
            throw new NotImplementedException();
        }
    }
}
