using ApiServico.DataContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServico.Controllers
{
    [Route("/prioridades")]
    [ApiController]
    public class PrioridadeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrioridadeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var prioridades = await _context.
                Prioridades.ToListAsync();

            return Ok(prioridades);
        }
    }
}
