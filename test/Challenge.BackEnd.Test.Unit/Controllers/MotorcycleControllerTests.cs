using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Interfaces.Services;
using Challenge.BackEnd.Presentation.API.Controllers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Challenge.BackEnd.Test.Unit.Unit.Controllers;

public class MotorcycleControllerTests
{
    private readonly Mock<IValidator<MotorcycleDto>> _validatorMock;
    private readonly Mock<IValidator<MotorcyclePlateUpdateDto>> _validatorUpMock;
    private readonly Mock<IMotorcycleService> _serviceMock;
    private readonly Mock<ILogger<MotorcycleController>> _loggerMock;

    private readonly MotorcycleController _controller;


    public MotorcycleControllerTests()
    {
        _validatorMock = new Mock<IValidator<MotorcycleDto>>();
        _validatorUpMock = new Mock<IValidator<MotorcyclePlateUpdateDto>>();
        _serviceMock = new Mock<IMotorcycleService>();
        _loggerMock = new Mock<ILogger<MotorcycleController>>();
        _controller = new MotorcycleController(_validatorMock.Object, _validatorUpMock.Object, _serviceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateMotorcycleAsync_ShouldReturnCreated_WhenValid()
    {
        var motorcycle = new MotorcycleDto { Id = "1", Model = "ModelX", Plate = "ABC123", FabricationYear = 2020 };
        _validatorMock.Setup(v => v.ValidateAsync(motorcycle, default)).ReturnsAsync(new ValidationResult());
        _serviceMock.Setup(s => s.CreateMotorcycleAsync(motorcycle)).ReturnsAsync(true);

        var result = await _controller.CreateMotorcycleAsync(motorcycle);

        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task CreateMotorcycleAsync_ShouldReturnBadRequest_WhenInvalid()
    {
        var motorcycle = new MotorcycleDto();
        _validatorMock.Setup(v => v.ValidateAsync(motorcycle, default)).ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Model", "Required") }));

        var result = await _controller.CreateMotorcycleAsync(motorcycle);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetMotorcycleByPlateAsync_ShouldReturnOk_WhenFound()
    {
        var motorcycle = new MotorcycleDto { Id = "1", Plate = "ABC123" };
        _serviceMock.Setup(s => s.GetMotorcycleByPlateAsync("ABC123")).ReturnsAsync(motorcycle);

        var result = await _controller.GetMotorcycleByPlateAsync("ABC123");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetMotorcycleByPlateAsync_ShouldReturnNotFound_WhenNotFound()
    {
        _serviceMock.Setup(s => s.GetMotorcycleByPlateAsync("ABC123")).ReturnsAsync((MotorcycleDto)null);

        var result = await _controller.GetMotorcycleByPlateAsync("ABC123");

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task MotosDeleteAsync_ShouldReturnOk_WhenDeleted()
    {
        _serviceMock.Setup(s => s.DeleteMotorcycleByIdAsync("1")).ReturnsAsync(true);

        var result = await _controller.MotosDeleteAsync("1");

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task MotosDeleteAsync_ShouldReturnBadRequest_WhenNotDeleted()
    {
        _serviceMock.Setup(s => s.DeleteMotorcycleByIdAsync("1")).ReturnsAsync(false);

        var result = await _controller.MotosDeleteAsync("1");

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
