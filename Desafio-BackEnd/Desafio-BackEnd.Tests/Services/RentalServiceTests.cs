using AutoMapper;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Models;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Interfaces.Repositories;
using Desafio_BackEnd.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Desafio_BackEnd.Tests.Services
{
    public class RentalServiceTests
    {
        private readonly Mock<IRentalRepository> _rentalRepositoryMock;
        private readonly Mock<IDeliveryManRepository> _deliveryManRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<RentalService>> _loggerMock;
        private readonly RentalService _rentalService;

        public RentalServiceTests()
        {
            _rentalRepositoryMock = new Mock<IRentalRepository>();
            _deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<RentalService>>();
            _rentalService = new RentalService(
                _rentalRepositoryMock.Object,
                _deliveryManRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task CreateRentalAsync_ShouldReturnTrue_WhenRentalIsCreated()
        {
            // Arrange
            var createRentalModel = new CreateRentalModel
            {
                DeliveryManId = "1",
                MotorcycleId = "M1",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(7),
                ExpectedEndDate = DateTime.UtcNow.AddDays(7),
                Plan = 7
            };

            var rental = new Rental
            {
                Id = 1,
                DeliveryManId = "1",
                MotorcycleId = "M1",
                StartDate = createRentalModel.StartDate,
                EndDate = createRentalModel.EndDate,
                ExpectedEndDate = createRentalModel.ExpectedEndDate,
                Plan = createRentalModel.Plan,
                DailyValue = 30.0m,
                CreatedDate = DateTime.UtcNow
            };

            _deliveryManRepositoryMock.Setup(r => r.GetDeliveryManByIdAsync("1"))
                .ReturnsAsync(new DeliveryMan { DriversLicenseCategory = "A" });
            _mapperMock.Setup(m => m.Map<Rental>(createRentalModel)).Returns(rental);
            _rentalRepositoryMock.Setup(r => r.CreateRentalAsync(rental)).ReturnsAsync(true);

            // Act
            var result = await _rentalService.CreateRentalAsync(createRentalModel);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetRentalByIdAsync_ShouldReturnRentalDto_WhenRentalExists()
        {
            // Arrange
            var rental = new Rental
            {
                Id = 1,
                DeliveryManId = "1",
                MotorcycleId = "M1",
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow,
                ExpectedEndDate = DateTime.UtcNow,
                Plan = 7,
                DailyValue = 30.0m,
                CreatedDate = DateTime.UtcNow.AddDays(-7)
            };

            var rentalDto = new RentalDto
            {
                Id = "1",
                DeliveryManId = "1",
                MotorCycleId = "M1",
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                ExpectedEndDate = rental.ExpectedEndDate,
                ReturnDate = rental.ReturnDate,
                DailyValue = rental.DailyValue
            };

            _rentalRepositoryMock.Setup(r => r.GetRentalByIdAsync(1)).ReturnsAsync(rental);
            _mapperMock.Setup(m => m.Map<RentalDto>(rental)).Returns(rentalDto);

            // Act
            var result = await _rentalService.GetRentalByIdAsync("locacao1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }

        [Fact]
        public async Task UpdateRentalReturnByIdAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var rental = new Rental
            {
                Id = 1,
                DeliveryManId = "1",
                MotorcycleId = "M1",
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow,
                ExpectedEndDate = DateTime.UtcNow.AddDays(-1),
                Plan = 7,
                DailyValue = 30.0m,
                CreatedDate = DateTime.UtcNow.AddDays(-7)
            };

            _rentalRepositoryMock.Setup(r => r.GetRentalByIdAsync(1)).ReturnsAsync(rental);
            _rentalRepositoryMock.Setup(r => r.UpdateRentalReturnByIdAsync(rental)).ReturnsAsync(true);

            // Act
            var result = await _rentalService.UpdateRentalReturnByIdAsync("locacao1", DateTime.UtcNow);

            // Assert
            Assert.True(result);
        }
    }
}
