using Desafio_BackEnd.Data;
using Desafio_BackEnd.Data.Repositories;
using Desafio_BackEnd.Utils.Fakes;

namespace Desafio_BackEnd.Tests.Integration.Repositories
{
    public class RentalRepositoryTests
    {
        private readonly RentalRepository _repository;
        private readonly MotorcycleRepository _motorcycleRepository;
        private readonly DeliveryManRepository _deliveryManRepository;

        private const int _rentalIdDefault = 1;
        private const string _deliveryManIdDefault = "entregador123";
        private const string _motorcycleIdDefault = "moto123";

        public RentalRepositoryTests()
        {
            var _databaseConnection = new DatabaseConnection("Host=localhost;Port=5432;Database=Challenge;Username=user_challenge;Password=159357*");

            _repository = new RentalRepository(_databaseConnection);
            _motorcycleRepository = new MotorcycleRepository(_databaseConnection);
            _deliveryManRepository = new DeliveryManRepository(_databaseConnection);
        }

        [Fact]
        public async Task CreateRentalAsync_ShouldInsertRental()
        {
            var motorcycleExists = await _motorcycleRepository.GetMotorcycleByIdAsync(_motorcycleIdDefault);
            Assert.NotNull(motorcycleExists);

            var deliveryManExists = await _deliveryManRepository.GetDeliveryManByIdAsync(_deliveryManIdDefault);
            Assert.NotNull(deliveryManExists);


            var rentalFake = new RentalFaker(_deliveryManIdDefault, _motorcycleIdDefault).Generate();

            var result = await _repository.CreateRentalAsync(rentalFake);

            Assert.True(result);
        }

        [Fact]
        public async Task GetRentalByIdAsync_ShouldReturnRental_WhenExists()
        {
            var result = await _repository.GetRentalByIdAsync(_rentalIdDefault);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetRentalByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var rentalFake = new RentalFaker().Generate();

            var result = await _repository.GetRentalByIdAsync(rentalFake.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateRentalReturnByIdAsync_ShouldUpdateReturnDate()
        {
            var rentalFake = new RentalFaker(50).Generate();

            rentalFake.Id = _rentalIdDefault;
            rentalFake.ReturnDate = DateTime.UtcNow;

            var result = await _repository.UpdateRentalReturnByIdAsync(rentalFake);

            Assert.True(result);

        }
    }
}
