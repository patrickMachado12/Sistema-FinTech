using FinTech.Api.Contract.NaturezaLancamento;

namespace FinTech.Api.Contract.AReceber
{
    public class AReceberResponseContract : AReceberRequestContract
    {
        public long Id { get; set; }
        public double ValorAReceber { get; set; }
        public double ValorBaixado { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public NaturezaLancamentoResponseContract NaturezaLancamento { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataRecebimento { get; set; }
        
    }

}
