using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Controllers
{
    [Route("locacao")]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly IValidator<RentalDto> _validator;


        public RentalController(IRentalService rentalService, IValidator<RentalDto> validator)
        {
            _rentalService = rentalService;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRentalAsync([FromBody] RentalDto rental)
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RentalReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRentalByIdAsync(string id)
        {
            var rentalReturn = await _rentalService.GetRentalByIdAsync(id);

            return Ok(rentalReturn);
        }

        /// <summary>
        /// Informar data de devolução e calcular valor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rentalReturn"></param>
        /// <returns></returns>
        [HttpPut("{id}/devolucao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendRentalReturnByIdAsync(string id, [FromBody] RentalReturnDate rentalReturnDate)
        {

            await _rentalService.SendRentalReturnByIdAsync(id, rentalReturnDate);

            return Ok();
        }
    }
}
