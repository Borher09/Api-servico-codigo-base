using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aula2ApiServico.Controllers
{
    [Route("/")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        [HttpGet]
        public IActionResult Principal()
        {
            var resultado = new { situacao = "OK", mensagem = "Api Serviço" };

            return Ok(resultado);

        }
        [HttpGet("Autor")]
        public IActionResult Autor()
        {
            var resultado = new { nome = "Ezequiel Borher", email = "ezequiel@gmail", contato= "69993902117"};

            return Ok(resultado);

        }
    }

}
