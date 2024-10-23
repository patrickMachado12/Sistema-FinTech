using FinTech.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTech.Api.Data.Mappings
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("pessoa")
            .HasKey(p => p.Id);

            builder.Property(p => p.Nome)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.Telefone)
            .HasColumnType("VARCHAR")
            .IsRequired();
            
            builder.Property(p => p.DataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.Status)
            .HasColumnType("boolean")
            .IsRequired();
        }
    }
}