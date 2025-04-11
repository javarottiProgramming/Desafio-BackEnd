using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Controllers
{
    [Route("locação")]
    public class LocacaoController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Locacao()
        {
            return View();
        }

        [HttpPut("{id}/devolucao")]
        public IActionResult Devolucao(string id, [FromBody] object locacao)
        {
            return Ok();
        }

        [HttpPost("")]
        public IActionResult Locacao([FromBody] object locacao)
        {
            return Ok();
        }
    }
}
