using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Services
{
    public class DeliveryMenService : IDeliveryMenService
    {
        public async Task<bool> CreateDeliveryManAsync(DeliveryMan deliveryMan)
        {
            //TODO implementar a criação do entregador no banco de dados.
            //TODO implementar regras de negocio

            return await Task.FromResult(true);
        }

        public async Task<bool> SendDocumentImageAsync(string id, string documentImgBase64)
        {
            try
            {
                //TODO Extrair metodo

                //converter base64 para byte[]
                var fileBytes = Convert.FromBase64String(documentImgBase64);

                string fileExtension = GetImageExtension(fileBytes);

                if (fileExtension == null)
                {
                    throw new InvalidOperationException("Formato de imagem inválido. Apenas PNG e BMP são suportados.");
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

                //TODO Fazer chamada para base de dados para salvar a data de upload do arquivo (CreateDate | UpdateDate)

                return await Task.FromResult(true);

            }
            catch (Exception)
            {

                throw;
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