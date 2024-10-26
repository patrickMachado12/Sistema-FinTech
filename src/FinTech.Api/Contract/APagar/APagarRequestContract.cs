namespace FinTech.Api.Contract.APagar
{
    public class APagarRequestContract
    {
        public long IdPessoa { get; set; }
        public long IdNaturezaLancamento { get; set; }
        public double ValorAPagar { get; set; }
        public double ValorPago { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataReferencia { get; set; }

    }
}