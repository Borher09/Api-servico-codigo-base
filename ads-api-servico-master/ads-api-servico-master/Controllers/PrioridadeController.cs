using ApiServico.DataContexts; // Importa o namespace onde o AppDbContext está definido (conexão/contexto com o banco de dados).
using Microsoft.AspNetCore.Http; // Importa tipos relacionados a requisições e respostas HTTP.
using Microsoft.AspNetCore.Mvc; // Importa funcionalidades essenciais para criar Controllers e Actions de API (como [HttpGet], ControllerBase, IActionResult).
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // Importa as funcionalidades do Entity Framework Core, como o método 'ToListAsync()'.

namespace ApiServico.Controllers // Define o agrupamento lógico (namespace) para o Controller.
{
  
    [Route("/Prioridade")] // Define a rota base da API para este controller. A URL será /Prioridade.
    [ApiController] // Indica que esta classe é um Controller de API, ativando funcionalidades específicas do ASP.NET Core (como inferência de fontes de dados e validação automática).
    
    public class PrioridadeController : ControllerBase // Declara a classe do Controller, herdando de ControllerBase para ter acesso a funcionalidades básicas de manipulação de requisições HTTP e respostas.
    {
        private readonly AppDbContext _context; // Declara um campo privado e somente leitura para a conexão com o banco de dados (o Contexto do EF Core).

        public PrioridadeController(AppDbContext context) // Construtor da classe. O ASP.NET Core injeta automaticamente uma instância do AppDbContext aqui (Injeção de Dependência).
        {
            _context = context; // Atribui o contexto injetado ao campo privado.
        }

        // ----------------------------------------------------------------------------------

        [HttpGet] // Mapeia este método para requisições HTTP GET na rota base definida (GET /Prioridade).
        public async Task<IActionResult> BuscarTodos() // Método assíncrono para buscar todas as prioridades. Retorna um 'IActionResult' (resposta HTTP).
        {
            // Acessa o DbSet (tabela) de Prioridades no contexto do banco de dados (_context.Prioridades).
            // .ToListAsync() é um método assíncrono do Entity Framework Core que executa a consulta no DB e retorna o resultado como uma lista.
            var prioridades = await _context.Prioridades.ToListAsync();

            // Retorna o código de status HTTP 200 (OK) junto com a lista de prioridades encontrada.
            return Ok(prioridades);
        }
    }
}