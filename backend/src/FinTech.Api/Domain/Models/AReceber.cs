using System.ComponentModel.DataAnnotations;

namespace FinTech.Api.Domain.Models
{
    public class AReceber
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long IdPessoa { get; set; }

        public Pessoa Pessoa { get; set; }

        [Required]
        public long IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        [Required]
        public long IdNaturezaLancamento { get; set; }

        public NaturezaLancamento NaturezaLancamento { get; set; }

        [Required(ErrorMessage = "O campo de Valor é obrigatório.")]
        public double ValorAReceber { get; set; }

        [Required(ErrorMessage = "O campo de Valor é obrigatório.")]
        public double ValorBaixado { get; set; }

        [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo DataEmissao é obrigatório.")]
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "O campo DataVencimento é obrigatório.")]
        public DateTime DataVencimento { get; set; }

        public DateTime? DataBaixa { get; set; }

        public DateTime? DataReferencia { get; set; }

        public DateTime? DataExclusao { get; set; }

        public string? Observacao { get; set; } = string.Empty;
    }
}