using AutoMapper;
using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Entities;
using Challenge.BackEnd.Core.Domain.Interfaces.Repositories;
using Challenge.BackEnd.Core.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Challenge.BackEnd.Test.Unit.Unit.Services
{
    public class DeliveryManServiceTests
    {
        private readonly Mock<IDeliveryManRepository> _deliveryManRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<DeliveryManService>> _loggerMock;
        private readonly DeliveryManService _deliveryManService;

        public DeliveryManServiceTests()
        {
            _deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<DeliveryManService>>();
            _deliveryManService = new DeliveryManService(
                _deliveryManRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task CreateDeliveryManAsync_ShouldReturnTrue_WhenDeliveryManIsCreated()
        {
            // Arrange
            var deliveryManDto = new DeliveryManDto
            {
                Id = "1",
                Name = "John Doe",
                Document = "123456789",
                BirthDate = new DateTime(1990, 1, 1),
                DriversLicense = "DL12345",
                DriversLicenseCategory = "A"
            };

            var deliveryMan = new DeliveryMan
            {
                Id = "1",
                Name = "John Doe",
                Document = "123456789",
                BirthDate = new DateTime(1990, 1, 1),
                DriversLicense = "DL12345",
                DriversLicenseCategory = "A",
                CreatedDate = DateTime.UtcNow
            };

            _mapperMock.Setup(m => m.Map<DeliveryMan>(deliveryManDto)).Returns(deliveryMan);
            _deliveryManRepositoryMock.Setup(r => r.CreateDeliveryManAsync(deliveryMan)).ReturnsAsync(true);

            // Act
            var result = await _deliveryManService.CreateDeliveryManAsync(deliveryManDto);

            // Assert
            Assert.True(result);
            _deliveryManRepositoryMock.Verify(r => r.CreateDeliveryManAsync(deliveryMan), Times.Once);
        }

        [Fact]
        public async Task UploadDocumentImageAsync_ShouldReturnTrue_WhenImageIsUploaded()
        {
            var id = "1";
            var documentImgBase64 = Convert.ToBase64String(new byte[] { 0x89, 0x50, 0x4E, 0x47 }); // PNG magic number

            var result = await _deliveryManService.UploadDocumentImageAsync(id, documentImgBase64);

            Assert.True(result);
        }

        [Fact]
        public async Task UploadDocumentImageAsync_ShouldThrowException_WhenImageFormatIsInvalid()
        {
            var id = "1";
            var documentImgBase64 = Convert.ToBase64String(new byte[] { 0x00, 0x00, 0x00, 0x00 }); // Invalid magic number

            var result = await _deliveryManService.UploadDocumentImageAsync(id, documentImgBase64);

            Assert.False(result);
        }
    }
}
