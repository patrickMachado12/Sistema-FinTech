using FinTech.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTech.Api.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario")
            .HasKey(p => p.Id);

            builder.Property(p => p.Email)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.Senha)
            .HasColumnType("VARCHAR")
            .IsRequired();
            
            builder.Property(p => p.DataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.DataInativacao)
            .HasColumnType("timestamp");

            builder.Property(p => p.Status)
            .HasColumnType("boolean")
            .IsRequired();
        }
    }
}