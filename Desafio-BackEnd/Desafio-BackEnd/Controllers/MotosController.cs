using Desafio_BackEnd.Domain.Interfaces;
using Desafio_BackEnd.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Desafio_BackEnd.Controllers
{
    [Route("[controller]")]
    public class MotosController : Controller
    {
        private readonly IValidator<Motorcycle> _validator;
        private readonly IMotorcycleService _motorcycleService;

        public MotosController(IValidator<Motorcycle> validator, IMotorcycleService motorcycleService)
        {
            _validator = validator;
            _motorcycleService = motorcycleService;
        }


        [HttpGet("")]
        public IActionResult Motos()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Motos(string id)
        {
            return Ok();
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(Motorcycle), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MotosAsync([FromBody] Motorcycle moto)
        {
            var result = await _validator.ValidateAsync(moto);

            if (!result.IsValid)
            {
                return BadRequest("Dados inválidos.");
            }

            await _motorcycleService.CreateMotorcycleAsync(moto);

            return Created();
        }

        [HttpPut("{id}/placa")]
        public IActionResult Motos(string id, [FromBody] object moto)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult MotosDelete(string id)
        {
            return Ok();
        }
    }
}