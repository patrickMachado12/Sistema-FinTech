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

        [Required(ErrorMessage = "O campo Descricao é obrigatório.")]
        public string Descricao { get; set; }

        public string Observacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo DataEmissao é obrigatório.")]
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "O campo DataVencimento é obrigatório.")]
        public DateTime DataVencimento { get; set; }

        public DateTime? DataRecebimento { get; set; }

    }
}