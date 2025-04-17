using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Interfaces.Services;
using Challenge.BackEnd.Core.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Challenge.BackEnd.Presentation.API.Controllers
{
    [ApiController]
    [Route("locacao")]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IValidator<CreateRentalModel> _validator;
        private readonly ILogger<RentalController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalController"/> class.
        /// </summary>
        /// <param name="rentalService">Service for rental operations.</param>
        /// <param name="validator">Validator for rental creation models.</param>
        public RentalController(IRentalService rentalService, IValidator<CreateRentalModel> validator, ILogger<RentalController> logger)
        {
            _rentalService = rentalService;
            _validator = validator;
            _logger = logger;
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
                    _logger.LogError(item);
                }

                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            await _rentalService.CreateRentalAsync(rental);

            return Created();

            //return CreatedAtAction(nameof(GetRentalByIdAsync), nameof(RentalController),
            //    new { id = rental.MotorcycleId });
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
        public async Task<IActionResult> UpdateRentalReturnByIdAsync(string id, [FromBody] RentalReturnDto rentalReturnDate)
        {
            var updated = await _rentalService.UpdateRentalReturnByIdAsync(id, rentalReturnDate.ReturnDate);

            if (!updated)
            {
                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            return Ok(new { mensagem = "Data de devolução informada com sucesso" });
        }
    }
}
