using FinTech.Api.Damain.Models;
using FinTech.Api.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinTech.Api.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}