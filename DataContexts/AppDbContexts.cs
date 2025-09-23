using aula2ApiServico.Models;
using Microsoft.EntityFrameworkCore;


namespace aula2ApiServico.DataContexts
{
    public class AppDbContexts: DbContext
    {

        public AppDbContexts(DbContextOptions options) : base(options) 
        {
        
  
        }
        public DbSet<Chamado> Chamados { get; set; }
    }
}
