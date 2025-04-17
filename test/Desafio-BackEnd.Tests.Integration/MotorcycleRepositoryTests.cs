using Challenge.BackEnd.Infrastructure.Data;
using Challenge.BackEnd.Infrastructure.Data.Repositories;
using Challenge.BackEnd.Core.Domain.Entities;
using Challenge.BackEnd.Core.Utils;
using Challenge.BackEnd.Core.Utils.Fakes;

namespace Challenge.BackEnd.Test.Unit.Integration.Repositories
{
    public class MotorcycleRepositoryTests
    {
        private readonly MotorcycleRepository _repository;
        private const string _motorcycleIdDefault = "moto123";
        private const string _motorcyclePlateDefault = "CDX-0101";

        public MotorcycleRepositoryTests()
        {
            var _databaseConnection = new DatabaseConnection("Host=localhost;Port=5432;Database=Challenge;Username=user_challenge;Password=159357*");
            _repository = new MotorcycleRepository(_databaseConnection);
        }

        [Fact]
        public async Task CreateMotorcycleAsync_ShouldInsertMotorcycle()
        {
            var motorcycleFake = new MotorcycleFaker().Generate();

            var result = await _repository.CreateMotorcycleAsync(motorcycleFake);

            Assert.True(result);

            var motorcycleFromDb = await _repository.GetMotorcycleByIdAsync(motorcycleFake.Id);

            Assert.NotNull(motorcycleFromDb);
        }

        [Fact]
        public async Task GetMotorcycleByIdAsync_ShouldReturnMotorcycle_WhenExists()
        {
            var result = await _repository.GetMotorcycleByIdAsync(_motorcycleIdDefault);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMotorcycleByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var motorcycleFake = new MotorcycleFaker().Generate();

            var result = await _repository.GetMotorcycleByIdAsync(motorcycleFake.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateMotorcyclePlateByIdAsync_ShouldUpdatePlate()
        {
            var motorcycleFaker = new MotorcycleFaker();
            var motorcycleFake = motorcycleFaker.Generate();

            await _repository.CreateMotorcycleAsync(motorcycleFake);

            var newPlate = motorcycleFaker.Generate().Plate;

            var result = await _repository.UpdateMotorcyclePlateByIdAsync(motorcycleFake.Id, newPlate);

            Assert.True(result);

            var updatedMotorcycle = await _repository.GetMotorcycleByIdAsync(motorcycleFake.Id);

            Assert.Equal(newPlate, updatedMotorcycle?.Plate);
        }

        [Fact]
        public async Task DeleteMotorcycleByIdAsync_ShouldDeleteMotorcycle()
        {
            var motorcycleFake = new MotorcycleFaker().Generate();
            await _repository.CreateMotorcycleAsync(motorcycleFake);

            var result = await _repository.DeleteMotorcycleByIdAsync(motorcycleFake.Id);

            Assert.True(result);

            var deletedMotorcycle = await _repository.GetMotorcycleByIdAsync(motorcycleFake.Id);

            Assert.Null(deletedMotorcycle);
        }
    }
}