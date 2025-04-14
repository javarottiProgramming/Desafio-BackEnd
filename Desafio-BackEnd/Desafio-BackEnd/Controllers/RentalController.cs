using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Desafio_BackEnd.Controllers
{
    [ApiController]
    [Route("locacao")]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IValidator<CreateRentalModel> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalController"/> class.
        /// </summary>
        /// <param name="rentalService">Service for rental operations.</param>
        /// <param name="validator">Validator for rental creation models.</param>
        public RentalController(IRentalService rentalService, IValidator<CreateRentalModel> validator)
        {
            _rentalService = rentalService;
            _validator = validator;
        }

        /// <summary>
        /// Alugar uma moto
        /// </summary>
        /// <param name="rental">The rental creation model.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRentalAsync([FromBody] CreateRentalModel rental)
        {
            var result = await _validator.ValidateAsync(rental);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                foreach (var item in errorMessages)
                {
                    Console.WriteLine(item);
                }

                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            await _rentalService.CreateRentalAsync(rental);

            return Created();
        }

        /// <summary>
        /// Consultar locação por id
        /// </summary>
        /// <param name="id">The ID of the rental.</param>
        /// <returns>An <see cref="IActionResult"/> containing the rental details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RentalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRentalByIdAsync(string id)
        {
            var rental = await _rentalService.GetRentalByIdAsync(id);

            if (rental == null)
            {
                return NotFound(new { mensagem = "Locação não encontrada" });
            }

            return Ok(rental);
        }

        /// <summary>
        /// Informar data de devolução e calcular valor
        /// </summary>
        /// <param name="id">The ID of the rental.</param>
        /// <param name="rentalReturnDate">The return date details.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPut("{id}/devolucao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRentalReturnByIdAsync(string id, [FromBody] RentalReturnDto rentalReturnDate)
        {
            var created = await _rentalService.CreateRentalReturnByIdAsync(id, rentalReturnDate);

            if (!created)
            {
                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            return Ok(new { mensagem = "Data de devolução informada com sucesso" });
        }
    }
}
