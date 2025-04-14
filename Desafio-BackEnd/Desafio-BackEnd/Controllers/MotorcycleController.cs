using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Controllers
{
    [ApiController]
    [Route("motos")]
    [Produces("application/json")]
    public class MotorcycleController : ControllerBase
    {
        private readonly IValidator<MotorcycleDto> _validator;
        private readonly IValidator<MotorcyclePlateUpdateDto> _validatorUp;
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleController(IValidator<MotorcycleDto> validator, IValidator<MotorcyclePlateUpdateDto> validatorUp, IMotorcycleService motorcycleService)
        {
            _validator = validator;
            _validatorUp = validatorUp;
            _motorcycleService = motorcycleService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMotorcycleAsync([FromBody] MotorcycleDto moto)
        {
            try
            {
                var result = await _validator.ValidateAsync(moto);

                if (!result.IsValid)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    foreach (var item in errorMessages)
                    {
                        Console.WriteLine(item);
                    }
                    return BadRequest("Dados inválidos.");
                }

                await _motorcycleService.CreateMotorcycleAsync(moto);

                return Created();
            }
            catch
            {
                return BadRequest("Dados inválidos.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(MotorcycleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMotorcycleByPlateAsync([FromQuery(Name = "placa")] string plate)
        {
            var moto = await _motorcycleService.GetMotorcycleByPlateAsync(plate);

            if (moto == null)
            {
                return NotFound("Moto não encontrada.");
            }

            return Ok(moto);
        }

        /// <summary>
        /// Updates the motorcycle plate by its ID.
        /// </summary>
        /// <param name="id">The ID of the motorcycle to update.</param>
        /// <param name="motorcyclePlateUpdateDto">The new plate information.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPut("{id}/placa")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMotorcyclePlateByIdAsync(string id, [FromBody] MotorcyclePlateUpdateDto motorcyclePlateUpdateDto)
        {
            /*
            var result = await _validatorUp.ValidateAsync(moto);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                foreach (var item in errorMessages)
                {
                    Console.WriteLine(item);
                }
                return BadRequest("Dados inválidos.");
            }
            */

            var motoUpdated = await _motorcycleService.UpdateMotorcyclePlateByIdAsync(id, motorcyclePlateUpdateDto.Plate);

            if (!motoUpdated)
            {
                return BadRequest("Dados inválidos");
            }

            return Ok(new { mensagem = "Placa modificada com sucesso" });
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotorcycleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMotorcycleByIdAsync(string id)
        {
            var moto = await _motorcycleService.GetMotorcycleByIdAsync(id);

            if (moto == null)
            {
                return NotFound("Moto não encontrada.");
            }

            return Ok(moto);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MotosDeleteAsync(string id)
        {
            var motoUpdated = await _motorcycleService.DeleteMotorcycleByIdAsync(id);

            if (!motoUpdated)
            {
                return BadRequest("Dados inválidos");
            }

            return Ok(new { mensagem = "Placa modificada com sucesso" });
        }
    }
}