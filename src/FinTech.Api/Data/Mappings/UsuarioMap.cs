using FinTech.Api.Damain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTech.Api.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario")
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

            builder.Property(p => p.Ativo)
            .HasColumnType("boolean")
            .IsRequired();
        }
    }
}