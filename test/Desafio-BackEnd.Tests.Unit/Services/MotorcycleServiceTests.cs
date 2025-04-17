using AutoMapper;
using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Entities;
using Challenge.BackEnd.Core.Domain.Interfaces.Repositories;
using Challenge.BackEnd.Core.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Challenge.BackEnd.Test.Unit.Unit.Services
{
    public class MotorcycleServiceTests
    {
        private readonly Mock<IMotorcycleRepository> _motorcycleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<MotorcycleService>> _loggerMock;
        private readonly MotorcycleService _motorcycleService;
        private readonly IBus _busMock;

        public MotorcycleServiceTests()
        {
            _motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();

            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<MotorcycleService>>();
            _busMock = Mock.Of<IBus>();

            _motorcycleService = new MotorcycleService(
                _motorcycleRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object,
                _busMock
            );
        }

        [Fact]
        public async Task CreateMotorcycleAsync_ShouldReturnTrue_WhenMotorcycleIsCreatedAndPlateIsUnique()
        {
            // Arrange
            var motorcycleDto = new MotorcycleDto
            {
                Id = "1",
                Plate = "ABC1234",
                FabricationYear = 2022,
                Model = "Model X"
            };
            var motorcycle = new Motorcycle
            {
                Id = "1",
                Plate = "ABC1234",
                FabricationYear = 2022,
                Model = "Model X",
                CreatedDate = DateTime.UtcNow
            };

            _mapperMock.Setup(m => m.Map<Motorcycle>(motorcycleDto)).Returns(motorcycle);
            _motorcycleRepositoryMock.Setup(r => r.GetMotorcycleByPlateAsync("ABC1234")).ReturnsAsync((Motorcycle?)null);
            _motorcycleRepositoryMock.Setup(r => r.CreateMotorcycleAsync(motorcycle)).ReturnsAsync(true);

            // Act
            var result = await _motorcycleService.CreateMotorcycleAsync(motorcycleDto);

            // Assert
            Assert.True(result);
            _motorcycleRepositoryMock.Verify(r => r.CreateMotorcycleAsync(motorcycle), Times.Once);
        }

        [Fact]
        public async Task GetMotorcycleByPlateAsync_ShouldReturnMotorcycleDto_WhenMotorcycleExists()
        {
            // Arrange
            var plate = "ABC1234";
            var motorcycle = new Motorcycle
            {
                Id = "1",
                Plate = plate,
                FabricationYear = 2022,
                Model = "Model X"
            };
            var motorcycleDto = new MotorcycleDto
            {
                Id = "1",
                Plate = plate,
                FabricationYear = 2022,
                Model = "Model X"
            };

            _motorcycleRepositoryMock.Setup(r => r.GetMotorcycleByPlateAsync(plate)).ReturnsAsync(motorcycle);
            _mapperMock.Setup(m => m.Map<MotorcycleDto>(motorcycle)).Returns(motorcycleDto);

            // Act
            var result = await _motorcycleService.GetMotorcycleByPlateAsync(plate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(plate, result.Plate);
        }

        [Fact]
        public async Task GetMotorcycleByIdAsync_ShouldReturnMotorcycleDto_WhenMotorcycleExists()
        {
            // Arrange
            var id = "1";
            var motorcycle = new Motorcycle
            {
                Id = id,
                Plate = "ABC1234",
                FabricationYear = 2022,
                Model = "Model X"
            };
            var motorcycleDto = new MotorcycleDto
            {
                Id = id,
                Plate = "ABC1234",
                FabricationYear = 2022,
                Model = "Model X"
            };

            _motorcycleRepositoryMock.Setup(r => r.GetMotorcycleByIdAsync(id)).ReturnsAsync(motorcycle);
            _mapperMock.Setup(m => m.Map<MotorcycleDto>(motorcycle)).Returns(motorcycleDto);

            // Act
            var result = await _motorcycleService.GetMotorcycleByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task UpdateMotorcyclePlateByIdAsync_ShouldReturnTrue_WhenPlateIsUnique()
        {
            // Arrange
            var id = "1";
            var plate = "XYZ5678";

            _motorcycleRepositoryMock.Setup(r => r.GetMotorcycleByPlateAsync(plate)).ReturnsAsync((Motorcycle?)null);
            _motorcycleRepositoryMock.Setup(r => r.UpdateMotorcyclePlateByIdAsync(id, plate)).ReturnsAsync(true);

            // Act
            var result = await _motorcycleService.UpdateMotorcyclePlateByIdAsync(id, plate);

            // Assert
            Assert.True(result);
            _motorcycleRepositoryMock.Verify(r => r.UpdateMotorcyclePlateByIdAsync(id, plate), Times.Once);
        }

        [Fact]
        public async Task DeleteMotorcycleByIdAsync_ShouldReturnTrue_WhenMotorcycleIsNotLinkedToRental()
        {
            // Arrange
            var id = "1";

            _motorcycleRepositoryMock.Setup(r => r.GetMotorcycleByIdAsync(id)).ReturnsAsync(new Motorcycle { Id = id });
            _motorcycleRepositoryMock.Setup(r => r.DeleteMotorcycleByIdAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _motorcycleService.DeleteMotorcycleByIdAsync(id);

            // Assert
            Assert.True(result);
            _motorcycleRepositoryMock.Verify(r => r.DeleteMotorcycleByIdAsync(id), Times.Once);
        }
    }
}