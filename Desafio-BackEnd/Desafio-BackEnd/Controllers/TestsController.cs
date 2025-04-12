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

    }

    public class FileUpload
    {
        public IFormFile File { get; set; }
    }
}
