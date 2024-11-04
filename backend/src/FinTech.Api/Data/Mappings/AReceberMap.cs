using FinTech.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTech.Api.Data.Mappings
{
    public class AReceberMap : IEntityTypeConfiguration<AReceber>
    {
        public void Configure(EntityTypeBuilder<AReceber> builder)
        {
            builder.ToTable("AReceber")
            .HasKey(p => p.Id);

            builder.HasOne(p => p.Pessoa)
            .WithMany()
            .HasForeignKey(fk => fk.IdPessoa);

            builder.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(fk => fk.IdUsuario);

            builder.HasOne(p => p.NaturezaLancamento)
            .WithMany()
            .HasForeignKey(fk => fk.IdNaturezaLancamento);

            builder.Property(p => p.Descricao)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.ValorAReceber)
            .HasColumnType("double precision")
            .IsRequired();

            builder.Property(p => p.ValorBaixado)
            .HasColumnType("double precision");

            builder.Property(p => p.DataEmissao)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.DataVencimento)
            .HasColumnType("timestamp")
            .IsRequired();           

            builder.Property(p => p.DataRecebimento)
            .HasColumnType("timestamp");

            builder.Property(p => p.DataReferencia)
            .HasColumnType("timestamp");

            builder.Property(p => p.Observacao)
            .HasColumnType("VARCHAR"); 
        }
    }
}