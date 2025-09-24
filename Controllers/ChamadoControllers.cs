using Microsoft.AspNetCore.Http; // Importa funcionalidades relacionadas a requisições HTTP.
using Microsoft.AspNetCore.Mvc; // Importa funcionalidades para criar APIs com controllers.
using aula2ApiServico.Models.Dtos; // Importa o namespace onde está o DTO usado para transferir dados.
using aula2ApiServico.DataContexts;
using Microsoft.EntityFrameworkCore;
namespace aula2ApiServico.Controllers // Define o namespace onde o controller está localizado.
{
    [Route("/chamados")] // Define a rota base da controller como "/chamados".
    [ApiController] // Indica que esta classe é um controller de API (fornece validações automáticas, etc.).
    public class ChamadoControllers : ControllerBase // Define a classe do controller herdando de ControllerBase.
    {

        private readonly AppDbContexts _context;

        public ChamadoControllers(AppDbContexts contexts)
        {
            _context = contexts;
        }
        // Lista estática que simula um "banco de dados" em memória com dois chamados.
        private static List<Chamado> _ListaChamados = new List<Chamado>
        {
            new Chamado() {Id = 1, Titulo ="erro na trela de acesso", Descricao = "O Usuário não conseguiu"},
            new Chamado() {Id = 2, Titulo ="Sistema com lentidão", Descricao = "Demora no carregamento da tela"}
        };

        // Variável estática que armazena o próximo ID a ser utilizado ao criar um novo chamado.
        private static int _proximoId = 3;

        [HttpGet] // Define que este método responde a requisições HTTP GET para /chamados.
        public async Task<IActionResult> BuscarTodos([FromQuery] string? search, [FromQuery] string? situacao)
        {
            var query = _context.Chamados.AsQueryable();


            if (search is not null)
            {
                query = query.Where(x => x.Titulo.Contains(search));

            }
            if (situacao is not null)
            {
                query = query.Where(x => x.situacao.Equals(situacao));
            }
            var chamados = await query.ToListAsync();

            return Ok(chamados);
        }
        [HttpGet("{id}")] // Define que este método responde a GET com um parâmetro de rota (ex: /chamados/1).
        public IActionResult BuscarPorId(int id)
        {
            // Procura um chamado na lista com o ID correspondente.
            var chamado = _ListaChamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null) // Se não encontrar, retorna 404 (Not Found).
            {
                return NotFound();
            }

            return Ok(chamado); // Se encontrar, retorna o chamado com status 200 (OK).
        }

        [HttpPost] // Define que este método responde a requisições HTTP POST para /chamados.
        public async Task<IActionResult> Criar([FromBody] ChamadoDto novoChamado)
        {

            // Cria um novo objeto Chamado com os dados recebidos via DTO.
            var chamado = new Chamado() { Titulo = novoChamado.titulo, Descricao = novoChamado.Descricao };
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();

            return Created("", new { });

        }

        [HttpPut("{id}")] // Define que este método responde a PUT com um ID na rota (ex: /chamados/1).
        public async Task<IActionResult> Atualizar(int id, [FromBody] ChamadoDto atualizarChamado)
        {
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);


            if (chamado is null)
            {
                return NotFound();
            }

            chamado.Titulo = atualizarChamado.titulo;
            chamado.Descricao = atualizarChamado.Descricao;

            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();

            return Ok(chamado);
        }


        [HttpPost("{id}/Finalizar")]
        public IActionResult FinalizarChamado(int id)
        {
            var chamado = _ListaChamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null)
            {
                return NotFound();
            }

            chamado.situacao = "Finalizado";
            return Ok(chamado.situacao);
        }

        [HttpDelete("{id}")] // Define rota DELETE para remover um chamado pelo ID.

        public async Task<IActionResult> Remover(int id, [FromBody] ChamadoDto atualizarChamado)
        {
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);


            if (chamado is null)
            {
                return NotFound();
            }

            _context.Chamados.Remove(chamado);
            await _context.SaveChangesAsync();

            return Ok(chamado);
        }




    }
}
