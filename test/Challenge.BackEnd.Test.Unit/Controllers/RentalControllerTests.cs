using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Interfaces.Services;
using Challenge.BackEnd.Core.Domain.Models;
using Challenge.BackEnd.Presentation.API.Controllers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Challenge.BackEnd.Test.Unit.Unit.Controllers;

public class RentalControllerTests
{
    private readonly Mock<IValidator<CreateRentalModel>> _validatorMock;
    private readonly Mock<IRentalService> _serviceMock;
    private readonly Mock<ILogger<RentalController>> _loggerMock;

    private readonly RentalController _controller;

    public RentalControllerTests()
    {
        _validatorMock = new Mock<IValidator<CreateRentalModel>>();
        _serviceMock = new Mock<IRentalService>();
        _loggerMock = new Mock<ILogger<RentalController>>();
        _controller = new RentalController(_serviceMock.Object, _validatorMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateRentalAsync_ShouldReturnCreated_WhenValid()
    {
        var rental = new CreateRentalModel
        {
            DeliveryManId = "1",
            MotorcycleId = "1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            ExpectedEndDate = DateTime.Now.AddDays(1),
            Plan = 7
        };
        _validatorMock.Setup(v => v.ValidateAsync(rental, default)).ReturnsAsync(new ValidationResult());
        _serviceMock.Setup(s => s.CreateRentalAsync(rental)).ReturnsAsync(true);

        var result = await _controller.CreateRentalAsync(rental);

        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task CreateRentalAsync_ShouldReturnBadRequest_WhenInvalid()
    {
        var rental = new CreateRentalModel
        {
            DeliveryManId = "",
            MotorcycleId = "1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            ExpectedEndDate = DateTime.Now.AddDays(1),
            Plan = 7
        };
        _validatorMock.Setup(v => v.ValidateAsync(rental, default)).ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("DeliveryManId", "Required") }));

        var result = await _controller.CreateRentalAsync(rental);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetRentalByIdAsync_ShouldReturnOk_WhenFound()
    {
        var rental = new RentalDto
        {
            Id = "1",
            DeliveryManId = "1",
            MotorCycleId = "1",
            StartDate = DateTime.Now,
            ExpectedEndDate = DateTime.Now.AddDays(1),
            DailyValue = 100
        };
        _serviceMock.Setup(s => s.GetRentalByIdAsync("1")).ReturnsAsync(rental);

        var result = await _controller.GetRentalByIdAsync("1");

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetRentalByIdAsync_ShouldReturnNotFound_WhenNotFound()
    {
        _serviceMock.Setup(s => s.GetRentalByIdAsync("1")).ReturnsAsync((RentalDto)null);

        var result = await _controller.GetRentalByIdAsync("1");

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdateRentalReturnByIdAsync_ShouldReturnOk_WhenUpdated()
    {
        _serviceMock.Setup(s => s.UpdateRentalReturnByIdAsync("1", It.IsAny<DateTime>())).ReturnsAsync(true);

        var result = await _controller.UpdateRentalReturnByIdAsync("1", new RentalReturnDto
        {
            ReturnDate = DateTime.Now
        });

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateRentalReturnByIdAsync_ShouldReturnBadRequest_WhenNotUpdated()
    {
        _serviceMock.Setup(s => s.UpdateRentalReturnByIdAsync("1", It.IsAny<DateTime>())).ReturnsAsync(false);

        var result = await _controller.UpdateRentalReturnByIdAsync("1", new RentalReturnDto
        {
            ReturnDate = DateTime.Now
        });

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
