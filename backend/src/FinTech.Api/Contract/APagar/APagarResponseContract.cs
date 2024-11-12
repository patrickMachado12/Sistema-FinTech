using System.Text.Json.Serialization;
using FinTech.Api.Contract.NaturezaLancamento;

namespace FinTech.Api.Contract.APagar
{
    public class APagarResponseContract : APagarRequestContract
    {
        public long Id { get; set; }
        public double ValorAPagar { get; set; }
        public double ValorPago { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public NaturezaLancamentoResponseContract NaturezaLancamento { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }   
    }
}
