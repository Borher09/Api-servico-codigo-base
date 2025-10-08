using Microsoft.AspNetCore.Http; // Importa tipos relacionados a requisições e respostas HTTP.
using Microsoft.AspNetCore.Mvc; // Importa funcionalidades essenciais para criar Controllers e Actions de API (como [HttpGet], IActionResult).
using ApiServico.Models.Dtos; // Importa a definição de objetos DTOs (Data Transfer Objects), provavelmente o 'ChamadoDto'.
using ApiServico.Models; // Importa as classes de modelo de dados, provavelmente a classe 'Chamado'.
using ApiServico.DataContexts; // Importa o contexto do banco de dados, 'AppDbContext'.
using Microsoft.EntityFrameworkCore; // Importa as funcionalidades do Entity Framework Core, como métodos 'Include', 'ToListAsync', 'FirstOrDefaultAsync'.
using ApiServico.Controllers;
using System.Runtime.InteropServices; // Importa o namespace do próprio controller (opcional, mas comum).

namespace ApiServico.Controllers // Define o agrupamento lógico (namespace) para o controller.
{
    [Route("/chamados")] // Define a rota base da API para este controller. Ex: GET /chamados, POST /chamados.
    [ApiController] // Indica que esta classe é um Controller de API, ativando comportamentos específicos do ASP.NET Core.
    public class ChamadoController : ControllerBase // Declara a classe do Controller, herdando de ControllerBase para funcionalidades básicas de API.
    {
        private readonly AppDbContext _context; // Declara um campo privado e somente leitura para a conexão com o banco de dados (o Contexto do EF Core).

        public ChamadoController(AppDbContext context) // Construtor da classe. O AppDbContext é injetado automaticamente pelo ASP.NET Core (Injeção de Dependência).
        {
            _context = context; // Atribui o contexto injetado ao campo privado.
        }

        // ----------------------------------------------------------------------------------

        [HttpGet] // Mapeia este método para requisições HTTP GET na rota base (/chamados).
        public async Task<IActionResult> BuscarTodos( // Método assíncrono para buscar todos os chamados. Retorna um 'IActionResult' (resposta HTTP).
            [FromQuery] string? search, // Define um parâmetro opcional 'search' vindo da query string (Ex: /chamados?search=texto).
            [FromQuery] string? situacao // Define um parâmetro opcional 'situacao' vindo da query string (Ex: /chamados?situacao=Aberto).
        )
        {
            var query = _context.Chamados.AsQueryable(); // Começa a construir a consulta LINQ (Language Integrated Query) usando o Entity Framework.

            if (search is not null) // Verifica se o parâmetro 'search' foi fornecido na requisição.
            {
                // Se sim, adiciona uma cláusula WHERE à consulta para filtrar chamados cujo Título contenha o valor de 'search'.
                query = query.Where(x => x.Titulo.Contains(search));
            }

            if (situacao is not null) // Verifica se o parâmetro 'situacao' foi fornecido na requisição.
            {
                // Se sim, adiciona outra cláusula WHERE à consulta para filtrar chamados cujo Status seja exatamente igual a 'situacao'.
                query = query.Where(x => x.Status.Equals(situacao));
            }

            // Executa a consulta no banco de dados:
            // .Include(p => p.Prioridade): Inclui os dados relacionados de 'Prioridade' junto com o 'Chamado' (Evita N+1 problem).
            // .ToListAsync(): Executa a consulta assincronamente e retorna a lista de chamados.
            var chamados = await query.Include(p => p.Prioridade).Select(c => new
            {
                c.Id,
                c.Titulo,
                c.Status,
                Prioridade = new { c.Prioridade.Nome }

            }).ToListAsync();


            return Ok(chamados); // Retorna a lista de chamados com o código de status HTTP 200 (OK).
        }

        // ----------------------------------------------------------------------------------

        [HttpGet("{id}")] // Mapeia este método para requisições HTTP GET com um ID na rota (Ex: /chamados/1).
        public async Task<IActionResult> BuscarPorId(int id) // Método para buscar um chamado pelo ID.
        {
            // Busca o primeiro chamado que corresponda ao ID fornecido. Se não encontrar, retorna null.
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);
            var prioridade = await _context.Prioridades.FirstOrDefaultAsync(p => p.Id == id);

            
            if (prioridade is null) // Verifica se o chamado não foi encontrado.
            {
                return NotFound(); // Retorna o código de status HTTP 404 (Not Found).
            }


            if (chamado is null) // Verifica se o chamado não foi encontrado.
            {
                return NotFound(); // Retorna o código de status HTTP 404 (Not Found).
            }

            return Ok(prioridade); // Retorna o chamado encontrado com o código de status HTTP 200 (OK).
        }

        // ----------------------------------------------------------------------------------

        [HttpPost] // Mapeia este método para requisições HTTP POST na rota base (/chamados). Usado para criar um novo recurso.
        public async Task<IActionResult> Criar([FromBody] ChamadoDto novoChamado) // Método para criar um novo chamado. Recebe um objeto DTO do corpo da requisição.
        {
            var prioridade = await _context.Prioridades.FirstOrDefaultAsync(x => x.Id == novoChamado.PrioridadeId);
            // Cria uma nova instância do modelo de banco de dados 'Chamado' a partir dos dados do DTO.
            var chamado = new Chamado() {
                Titulo = novoChamado.Titulo,
                Descricao = novoChamado.Descricao,
                
                PrioridadeId = novoChamado.PrioridadeId};

            await _context.Chamados.AddAsync(chamado); // Adiciona o novo chamado ao contexto do Entity Framework (em memória, ainda não no DB).
            await _context.SaveChangesAsync(); // Persiste as mudanças (o novo chamado) no banco de dados de forma assíncrona.

            // Retorna o código de status HTTP 201 (Created) e o objeto do chamado criado.
            return Created("", chamado);
        }

        // ----------------------------------------------------------------------------------

        [HttpPut("{id}")] // Mapeia este método para requisições HTTP PUT com um ID na rota (Ex: /chamados/1). Usado para atualizar um recurso existente.
        public async Task<IActionResult> Atualizar(int id, [FromBody] ChamadoDto atualizacaoChamado) // Método para atualizar um chamado. Recebe o ID e o DTO de atualização.
        {
            // Busca o chamado existente pelo ID no banco de dados.
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);

            if (chamado is null) // Verifica se o chamado não foi encontrado.
            {
                return NotFound(); // Retorna o código de status HTTP 404 (Not Found).
            }

            // Atualiza as propriedades do objeto 'chamado' existente com os novos valores vindos do DTO.
            chamado.Titulo = atualizacaoChamado.Titulo;
            chamado.Descricao = atualizacaoChamado.Descricao;

            _context.Chamados.Update(chamado); // Sinaliza para o Entity Framework que este objeto foi modificado (embora o EF geralmente detecte automaticamente).
            await _context.SaveChangesAsync(); // Salva as alterações de volta no banco de dados.

            return Ok(chamado); // Retorna o chamado atualizado com o código de status HTTP 200 (OK).
        }

        // ----------------------------------------------------------------------------------

        [HttpDelete("{id}")] // Mapeia este método para requisições HTTP DELETE com um ID na rota (Ex: /chamados/1). Usado para remover um recurso.
        public async Task<IActionResult> Remover(int id) // Método para remover um chamado pelo ID.
        {
            // Busca o chamado existente pelo ID no banco de dados.
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);

            if (chamado is null) // Verifica se o chamado não foi encontrado.
            {
                return NotFound(); // Retorna o código de status HTTP 404 (Not Found).
            }

            _context.Chamados.Remove(chamado); // Sinaliza para o Entity Framework que este objeto deve ser removido.
            await _context.SaveChangesAsync(); // Executa a remoção no banco de dados.

            // Retorna o código de status HTTP 204 (No Content), padrão para remoções bem-sucedidas onde não há corpo de resposta.
            return NoContent();
        }
    }
}