using FinTech.Api.Domain.Models;
using FinTech.Api.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinTech.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<NaturezaLancamento> NaturezaLancamento { get; set; }
        public DbSet<APagar> APagar { get; set; }
        public DbSet<AReceber> AReceber { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new NaturezaLancamentoMap());
            modelBuilder.ApplyConfiguration(new APagarMap());
            modelBuilder.ApplyConfiguration(new AReceberMap());

            modelBuilder.Entity<AReceber>()
                .HasOne(t => t.NaturezaLancamento)
                .WithMany()
                .HasForeignKey(t => t.IdNaturezaLancamento);

            modelBuilder.Entity<APagar>()
                .HasOne(t => t.NaturezaLancamento)
                .WithMany()
                .HasForeignKey(t => t.IdNaturezaLancamento);            
        }
    }
}