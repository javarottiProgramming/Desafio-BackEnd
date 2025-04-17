using AutoMapper;
using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Entities;
using Challenge.BackEnd.Core.Domain.Events;
using Challenge.BackEnd.Core.Domain.Interfaces.Repositories;
using Challenge.BackEnd.Core.Domain.Interfaces.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Challenge.BackEnd.Core.Services
{
    public class DeliveryManService : IDeliveryManService
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveryManService> _logger;

        public DeliveryManService(IDeliveryManRepository deliveryManRepository,
            IMapper mapper, ILogger<DeliveryManService> logger)
        {
            _deliveryManRepository = deliveryManRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> CreateDeliveryManAsync(DeliveryManDto deliveryManDto)
        {
            //TODO implementar regras de negocio

            try
            {
                var deliveryMan = _mapper.Map<DeliveryMan>(deliveryManDto);

                var inserted = await _deliveryManRepository.CreateDeliveryManAsync(deliveryMan);

                return await Task.FromResult(inserted);
            }
            catch (Npgsql.PostgresException ex)
            {
                _logger.LogError($"Constraint {ex.ConstraintName} violation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while creating delivery man: {ex.Message}");
            }

            return false;
        }

        public async Task<bool> UploadDocumentImageAsync(string id, string documentImgBase64)
        {
            try
            {
                //TODO Extrair metodo

                var fileBytes = Convert.FromBase64String(documentImgBase64);

                string fileExtension = GetImageExtension(fileBytes);

                if (fileExtension == null)
                {
                    _logger.LogError("Invalid image format. Only PNG and BMP are supported.");
                    return false;
                }


                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "uploads")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "uploads"));
                }

                //salvar o arquivo em um diretório

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", $"{id}.{fileExtension}");

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                }

                _logger.LogInformation($"Drivers License image uploaded to id: {id}");

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while upload delivery man document: {ex.Message}");
                return false;
            }
        }

        //TODO Melhorar
        private string? GetImageExtension(byte[] fileBytes)
        {
            // Verificar os primeiros bytes (magic numbers) para determinar o formato
            if (fileBytes.Length >= 4)
            {
                // PNG: 89 50 4E 47
                if (fileBytes[0] == 0x89 && fileBytes[1] == 0x50 && fileBytes[2] == 0x4E && fileBytes[3] == 0x47)
                {
                    return "png";
                }
                // BMP: 42 4D
                else if (fileBytes[0] == 0x42 && fileBytes[1] == 0x4D)
                {
                    return "bmp";
                }
            }

            // Retornar null se o formato não for reconhecido
            return null;
        }
    }
}