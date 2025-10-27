using ColetaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ColetaAPI.DataContext
{
    public class ApplicationsDbContext : DbContext

    {
        public ApplicationsDbContext(DbContextOptions<ApplicationsDbContext> options) : base(options)
        {
        }

        public DbSet<LocalizacaoModel> Localizacoes { get; set; }
        public DbSet<ColetaModel> Coletas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ColetaModel>()
                .HasOne(c => c.Localizacao)
                .WithMany(l => l.Coletas)
                .HasForeignKey(c => c.LocalizacaoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
