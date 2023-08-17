using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Infra.Map;
using Domain.Entities;
using Domain.Interfaces;

namespace Infra.Data
{
    public class DataContext : DbContext, ISalvar
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DataContext()
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Enquete> Enquete { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new EnqueteMap());
        }

        public async Task CommitAsync()
        {
            var cetZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Criado") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("Criado").CurrentValue = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("Alterado").IsModified = false;
                    entry.Property("Alterado").CurrentValue = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
                }
            }
            var x = await base.SaveChangesAsync();
        }
    }
}
