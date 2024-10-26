using System.ComponentModel.DataAnnotations;

namespace FinTech.Api.Contract.AReceber
{
    public class AReceberRequestContract
    {
        [Required(ErrorMessage = "O campo IdPessoa é obrigatório.")]
        public long IdPessoa { get; set; }

        [Required(ErrorMessage = "O campo IdNaturezaLancamento é obrigatório.")]
        public long IdNaturezaLancamento { get; set; }

        [Required(ErrorMessage = "O campo ValorAReceber é obrigatório.")]
        public double ValorAReceber { get; set; }

        public double ValorBaixado { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public string Observacao { get; set; } = string.Empty;

        public DateTime DataEmissao { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime? DataRecebimento { get; set; }
        
        public DateTime? DataReferencia { get; set; }

    }
}