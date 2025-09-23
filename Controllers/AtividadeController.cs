using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aula2ApiServico.Controllers
{
    [Route("Atividade")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {
        private static List<Atividade> ListarAtividade = new List<Atividade>() 
        {
            new Atividade() {id = 1, Descricao="atividade de web"}
        
        };

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok (ListarAtividade);
        }
    }
}
