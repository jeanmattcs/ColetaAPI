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

        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<CollectionModel> Collections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CollectionModel>()
                .HasOne(c => c.Location)
                .WithMany(l => l.Collections)
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
