using Desafio_BackEnd.Domain.Interfaces.Services;
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
        public IActionResult GetMotoByPlate()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetMotoById(string id)
        {
            return Ok();
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(Motorcycle), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMotosAsync([FromBody] Motorcycle moto)
        {
            var result = await _validator.ValidateAsync(moto);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                Console.WriteLine(errorMessages);
                //return BadRequest("Dados inválidos.");
                return BadRequest(errorMessages);
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