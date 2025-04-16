using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Desafio_BackEnd.Controllers;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;

namespace Desafio_BackEnd.Tests.Controllers;
public class DeliveryManControllerTests
{
    private readonly Mock<IValidator<DeliveryManDto>> _validatorMock;
    private readonly Mock<IDeliveryManService> _serviceMock;
    private readonly DeliveryManController _controller;

    public DeliveryManControllerTests()
    {
        _validatorMock = new Mock<IValidator<DeliveryManDto>>();
        _serviceMock = new Mock<IDeliveryManService>();
        _controller = new DeliveryManController(_validatorMock.Object, _serviceMock.Object);
    }

    [Fact]
    public async Task CreateDeliveryManAsync_ShouldReturnCreated_WhenValid()
    {
        var deliveryMan = new DeliveryManDto
        {
            Id = "1",
            Name = "John",
            Document = "123456789",
            BirthDate = DateTime.Now.AddYears(-30),
            DriversLicense = "DL12345",
            DriversLicenseCategory = "A",
            DriversLicenseBase64 = "base64string"
        };
        _validatorMock.Setup(v => v.ValidateAsync(deliveryMan, default)).ReturnsAsync(new ValidationResult());
        _serviceMock.Setup(s => s.CreateDeliveryManAsync(deliveryMan)).ReturnsAsync(true);

        var result = await _controller.CreateDeliveryManAsync(deliveryMan);

        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task CreateDeliveryManAsync_ShouldReturnBadRequest_WhenInvalid()
    {
        var deliveryMan = new DeliveryManDto
        {
            Id = "1",
            Name = "",
            Document = "123456789",
            BirthDate = DateTime.Now.AddYears(-30),
            DriversLicense = "DL12345",
            DriversLicenseCategory = "A"
        };
        _validatorMock.Setup(v => v.ValidateAsync(deliveryMan, default)).ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Name", "Required") }));

        var result = await _controller.CreateDeliveryManAsync(deliveryMan);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UploadDocumentImageAsync_ShouldReturnCreated_WhenValid()
    {
        var file = new DeliveryManDtoFileUpload { DocumentImgBase64 = "base64string" };
        _serviceMock.Setup(s => s.UploadDocumentImageAsync("1", file.DocumentImgBase64)).ReturnsAsync(true);

        var result = await _controller.UploadDocumentImageAsync("1", file);

        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task UploadDocumentImageAsync_ShouldReturnBadRequest_WhenInvalid()
    {
        var result = await _controller.UploadDocumentImageAsync("1", null);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
