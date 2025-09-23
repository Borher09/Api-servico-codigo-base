//using aula2ApiServico.Models.Dtos;
//using Microsoft.AspNetCore.Mvc;

//namespace aula2ApiServico
//{
//    public class txt
//    {
//                                                               Codigo Original


//            using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using aula2ApiServico.Models.Dtos;

//namespace aula2ApiServico.Controllers
//    {
//        [Route("/chamados")]
//        [ApiController]
//        public class ChamadoControllers : ControllerBase
//        {

//            private static List<Chamado> _ListaChamados = new List<Chamado>
//        {
//            new Chamado() {Id = 1, Titulo ="erro na trela de acesso", Descricao = "O Usuário não conseguiu"},
//            new Chamado() {Id = 2, Titulo ="Sistema com lentidão", Descricao = "Demora no carregamento da tela"}
//        };

//            private static int _proximoId = 3;

//            [HttpGet]
//            public IActionResult BuscarTodos()
//            {
//                return Ok(_ListaChamados);
//            }

//            [HttpGet("{id}")]
//            public IActionResult BuscarPorId(int id)
//            {

//                var chamado = _ListaChamados.FirstOrDefault(x => x.Id == id);
//                if (chamado == null)
//                {
//                    return NotFound();

//                }
//                return Ok(chamado);

//            }

//            [HttpPost]

//            public IActionResult Criar([FromBody] ChamadoDto novoChamado)
//            {
//                var chamado = new Chamado() { Titulo = novoChamado.titulo, Descricao = novoChamado.Descricao };

//                chamado.Id = _proximoId++;


//                _ListaChamados.Add(chamado);

//                return Created("", chamado);
//            }
//            [HttpPut("{id}")]
//            public IActionResult Atulizacao(int id, [FromBody] ChamadoDto novoChamado)
//            {
//                var chamado = _ListaChamados.FirstOrDefault(item => item.Id == id);
//                if (chamado == null)
//                {
//                    return NotFound();
//                }
//                chamado.Titulo = novoChamado.titulo;
//                chamado.Descricao = novoChamado.Descricao;


//                return Ok(chamado);
//            }

//            [HttpPut("{id}/status")]
//            public IActionResult AtulizarStatus(int id, [FromBody] Chamado Chamado)
//            {
//                var chamado = _ListaChamados.FirstOrDefault(item => item.Id == id);
//                if (chamado == null)
//                {
//                    return NotFound();
//                }
//               ;
//                chamado.Status = Chamado.Status;

//                return Ok(chamado.Status);
//            }

//            [HttpDelete("{id}")]
//            public IActionResult Remover(int id)
//            {

//                var chamado = _ListaChamados.FirstOrDefault(x => x.Id == id);
//                if (chamado == null)
//                {
//                    return NotFound();

//                }
//                _ListaChamados.Remove(chamado);

//                return NoContent();

//            }
//        }
//    }

//}
//}
