using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Desafio_BackEnd.Controllers
{
    [Route("[controller]")]
    public class MotosController : Controller
    {
        private readonly IValidator<Motorcycle> _validator;
        private readonly IValidator<MotorcycleUpdate> _validatorUp;
        private readonly IMotorcycleService _motorcycleService;

        public MotosController(IValidator<Motorcycle> validator, IValidator<MotorcycleUpdate> validatorUp, IMotorcycleService motorcycleService)
        {
            _validator = validator;
            _validatorUp = validatorUp;
            _motorcycleService = motorcycleService;
        }

        /// <summary>
        /// Cria uma nova moto
        /// </summary>
        /// <param name="moto"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMotosAsync([FromBody] Motorcycle moto)
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

        [HttpGet("{plate}")]
        [ProducesResponseType(typeof(Motorcycle), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMotoByPlateAsync(string plate)
        {
            var moto = await _motorcycleService.GetMotorcycleByPlateAsync(plate);

            if (moto == null)
            {
                return NotFound("Moto não encontrada.");
            }

            return Ok(moto);
        }

        [HttpPut("{id}/placa")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MotosAsync(string id, [FromBody] MotorcycleUpdate moto)
        {
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

            var motoUpdated = await _motorcycleService.UpdateMotorcycleAsync(id, moto);

            if (!motoUpdated)
            {
                return BadRequest("Dados inválidos");
            }

            return Ok("Placa modificada com sucesso");
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Motorcycle), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMotoByIdAsync(string id)
        {
            //TODO Implementar 400
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
            //TODO Implementar 400
            var moto = await _motorcycleService.DeleteMotorcycleAsync(id);

            return moto ? Ok() : BadRequest("Dados inválidos");

        }
    }
}