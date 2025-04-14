using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Controllers
{
    [ApiController]
    [Route("entregadores")]
    public class DeliveryMenController : ControllerBase
    {
        private readonly IValidator<DeliveryManRequest> _validator;
        private readonly IDeliveryMenService _deliveryMenService;

        public DeliveryMenController(IValidator<DeliveryManRequest> validator, IDeliveryMenService deliveryMenService)
        {
            _validator = validator;
            _deliveryMenService = deliveryMenService;
        }

        /// <summary>
        /// Cria um novo entregador
        /// </summary>
        /// <param name="deliveryMan"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDeliveryMenAsync([FromBody] DeliveryManRequest deliveryMan)
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

            await _deliveryMenService.CreateDeliveryManAsync(deliveryMan);

            return Created();
        }


        /// <summary>
        /// Envia foto do documento do entregador - CNH
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("/{id}/cnh")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendDocumentImageAsync(string id, [FromBody] DeliveryManFileUpload file)
        {
            //TODO validar formato da imagem e valido extensao png ou bmp
            //TODO corrigir parametro string($binary)

            if (file == null || string.IsNullOrEmpty(file.DocumentImgBase64))
            {
                return BadRequest("No file uploaded.");
            }


            // Chamar o serviço para enviar a imagem do documento
            var result = await _deliveryMenService.SendDocumentImageAsync(id, file.DocumentImgBase64);

            return Ok();
        }
    }
}
