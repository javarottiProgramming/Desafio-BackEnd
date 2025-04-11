using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.Controllers
{
    [Route("[controller]")]
    public class entregadoresController : Controller
    {
        [HttpPost("entregadores")]
        public IActionResult Entregadores([FromBody] object entregador)
        {
            return Ok();
        }

        [HttpPost("entregadores/{id}/cnh")]
        public IActionResult Entregadores(string id, [FromBody] object entregador)
        {
            return Ok();
        }
    }
}
