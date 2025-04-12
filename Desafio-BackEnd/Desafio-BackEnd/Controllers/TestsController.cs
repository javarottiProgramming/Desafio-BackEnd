using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Controllers
{
    [Route("testes")]
    public class TestsController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] FileUpload files)
        {
            string fileBase64;

            if (files == null || files.File == null)
            {
                return BadRequest("No file uploaded.");
            }

            using(var stream = new MemoryStream())
            {
                await files.File.CopyToAsync(stream);
                var fileBytes = stream.ToArray();

                var fileExtension = GetImageExtension(fileBytes);

                fileBase64 = Convert.ToBase64String(fileBytes);
                // Aqui você pode fazer o que quiser com o arquivo, como salvá-lo em um banco de dados ou sistema de arquivos

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "uploads")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "uploads"));
                }

                // Exemplo: salvar o arquivo em um diretório
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", files.File.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                }
            }


            return Ok(fileBase64);
        }
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

    public class FileUpload
    {
        public IFormFile File { get; set; }
    }
}
