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

            //validar se base64 é válida
            if (!file.DocumentImgBase64.StartsWith("data:image/png;base64,"))
            {
                return BadRequest("Invalid base64 string.");
            }

            //converter base64 para byte[]
            var fileBytes = Convert.FromBase64String(file.DocumentImgBase64);

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "uploads")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "uploads"));
            }

            // Exemplo: salvar o arquivo em um diretório
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", $"{id}");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(fileBytes, 0, fileBytes.Length);
            }
            return Ok();
        }
    }
}
