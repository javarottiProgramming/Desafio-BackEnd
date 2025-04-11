using Desafio_BackEnd.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Controllers
{
    [Route("entregadores")]
    public class DeliveryMenController : Controller
    {
        private readonly IValidator<DeliveryMan> _validator;

        public DeliveryMenController(IValidator<DeliveryMan> validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// Cria um novo entregador
        /// </summary>
        /// <param name="deliveryMan"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDeliveryMenAsync([FromBody] DeliveryMan deliveryMan)
        {
            var result = await _validator.ValidateAsync(deliveryMan);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                foreach (var item in errorMessages)
                {
                    Console.WriteLine(item);

                }
                return BadRequest("Dados inválidos.");
            }

            //await _motorcycleService.CreateMotorcycleAsync(moto);

            return Created();
            
        }


        /// <summary>
        /// Envia foto do documento do entregador - CNH
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("/{id}/cnh")]
        public async Task<IActionResult> SendDocumentImageAsync(string id, [FromBody] string base64String)//TODO verificar
        {
            return Ok();
        }
    }
}
