using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Controllers
{
    /// <summary>
    /// Controller for managing rental operations.
    /// </summary>
    [Route("locacao")]
    [Produces("application/json")]
    public class RentalController : Controller
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
        /// Creates a new rental.
        /// </summary>
        /// <param name="rental">The rental creation model.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

                return BadRequest("Dados inválidos.");
            }

            await _rentalService.CreateRentalAsync(rental);

            return Ok();
        }

        /// <summary>
        /// Retrieves a rental by its ID.
        /// </summary>
        /// <param name="id">The ID of the rental.</param>
        /// <returns>An <see cref="IActionResult"/> containing the rental details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RentalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRentalByIdAsync(string id)
        {
            var rentalReturn = await _rentalService.GetRentalByIdAsync(id);

            return Ok(rentalReturn);
        }

        /// <summary>
        /// Updates the rental with a return date and calculates the value.
        /// </summary>
        /// <param name="id">The ID of the rental.</param>
        /// <param name="rentalReturnDate">The return date details.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPut("{id}/devolucao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendRentalReturnByIdAsync(string id, [FromBody] RentalReturnDto rentalReturnDate)
        {
            await _rentalService.SendRentalReturnByIdAsync(id, rentalReturnDate);

            return Ok();
        }
    }
}
