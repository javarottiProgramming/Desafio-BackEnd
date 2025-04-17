using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Challenge.BackEnd.Presentation.API.Controllers
{
    [ApiController]
    [Route("motos")]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MotorcycleController : ControllerBase
    {
        private readonly IValidator<MotorcycleDto> _validator;
        private readonly IValidator<MotorcyclePlateUpdateDto> _validatorUp;
        private readonly IMotorcycleService _motorcycleService;
        private readonly ILogger<MotorcycleController> _logger;


        public MotorcycleController(IValidator<MotorcycleDto> validator, IValidator<MotorcyclePlateUpdateDto> validatorUp, IMotorcycleService motorcycleService, ILogger<MotorcycleController> logger)
        {
            _validator = validator;
            _validatorUp = validatorUp;
            _motorcycleService = motorcycleService;
            _logger = logger;
        }

        /// <summary>
        /// Cadastrar uma nova moto
        /// </summary>
        /// <param name="moto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMotorcycleAsync([FromBody] MotorcycleDto moto)
        {
            var result = await _validator.ValidateAsync(moto);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                var errorMessagesSplit = String.Join(" | ", errorMessages);

                _logger.LogError(errorMessagesSplit);

                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            var created = await _motorcycleService.CreateMotorcycleAsync(moto);

            if (!created)
            {
                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            return Created();
        }

        /// <summary>
        /// Consultar motos existentes
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(MotorcycleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMotorcycleByPlateAsync([FromQuery(Name = "placa")] string plate)
        {
            var moto = await _motorcycleService.GetMotorcycleByPlateAsync(plate);

            if (moto == null)
            {
                return NotFound(new { mensagem = "Moto não encontrada" });
            }

            return Ok(moto);
        }

        /// <summary>
        /// Modificar a placa de uma moto
        /// </summary>
        /// <param name="id">The ID of the motorcycle to update.</param>
        /// <param name="motorcyclePlateUpdateDto">The new plate information.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("{id}/placa")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMotorcyclePlateByIdAsync(string id, [FromBody] MotorcyclePlateUpdateDto motorcyclePlateUpdateDto)
        {
            var result = await _validatorUp.ValidateAsync(motorcyclePlateUpdateDto);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                var errorMessagesSplit = String.Join(" | ", errorMessages);

                _logger.LogError(errorMessagesSplit);

                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            var motoUpdated = await _motorcycleService.UpdateMotorcyclePlateByIdAsync(id, motorcyclePlateUpdateDto.Plate);

            if (!motoUpdated)
            {
                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            return Ok(new { mensagem = "Placa modificada com sucesso" });
        }

        /// <summary>
        /// Consultar motos existentes por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotorcycleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMotorcycleByIdAsync(string id)
        {
            var moto = await _motorcycleService.GetMotorcycleByIdAsync(id);

            if (moto == null)
            {
                return NotFound(new { mensagem = "Moto não encontrada" });
            }

            return Ok(moto);
        }

        /// <summary>
        /// Remover uma moto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MotosDeleteAsync(string id)
        {
            var motoUpdated = await _motorcycleService.DeleteMotorcycleByIdAsync(id);

            if (!motoUpdated)
            {
                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            return Ok();
        }
    }
}