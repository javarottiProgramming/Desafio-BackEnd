﻿using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Challenge.BackEnd.Presentation.API.Controllers
{
    [ApiController]
    [Route("entregadores")]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class DeliveryManController : ControllerBase
    {
        private readonly IValidator<DeliveryManDto> _validator;
        private readonly IDeliveryManService _deliveryManService;
        private readonly ILogger<DeliveryManController> _logger;


        public DeliveryManController(IValidator<DeliveryManDto> validator, IDeliveryManService deliveryManService, ILogger<DeliveryManController> logger)
        {
            _validator = validator;
            _deliveryManService = deliveryManService;
            _logger = logger;
        }

        /// <summary>
        /// Cadastrar entregador
        /// </summary>
        /// <param name="deliveryMan"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDeliveryManAsync([FromBody] DeliveryManDto deliveryMan)
        {
            var result = await _validator.ValidateAsync(deliveryMan);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                foreach (var item in errorMessages)
                {
                    _logger.LogError(item);
                }

                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            var created = await _deliveryManService.CreateDeliveryManAsync(deliveryMan);

            if (!created)
            {
                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            //var action = CreatedAtAction(nameof(CreateDeliveryManAsync), "", deliveryMan);
            return Created();
        }


        /// <summary>
        /// Enviar foto da CNH
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/cnh")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadDocumentImageAsync(string id, [FromBody] DeliveryManDtoFileUpload file)
        {
            
            if (file == null || string.IsNullOrEmpty(file.DocumentImgBase64))
            {
                return BadRequest(new { mensagem = "Dados inválidos." });
            }

            // Chamar o serviço para enviar a imagem do documento e salvar localmente
            var result = await _deliveryManService.UploadDocumentImageAsync(id, file.DocumentImgBase64);

            if(!result)
                return BadRequest(new { mensagem = "Dados inválidos." });

            return Created();
        }
    }
}
