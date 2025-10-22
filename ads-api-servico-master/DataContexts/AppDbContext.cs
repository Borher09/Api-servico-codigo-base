using ApiServico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServico.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Chamado> Chamados { get; set; }

        public DbSet<Prioridade> Prioridades { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chamado>()
                .HasMany(c => c.Usuarios)
                .WithMany(u => u.Chamados)
                .UsingEntity<Dictionary<string, string>>(
                "Chamado_usuario",
                f => f.HasOne<Usuario>().WithMany().HasForeignKey("id_usu_fk"),
                f => f.HasOne<Chamado>().WithMany().HasForeignKey("id_cha_fk"),
                f => f.ToTable("Chamado_usuario")
                );
        }

    }
}
