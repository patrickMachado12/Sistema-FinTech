namespace FinTech.Api.Contract.AReceber
{
    public class AReceberRequestContract
    {
        public long IdPessoa { get; set; }
        public long IdNaturezaLancamento { get; set; }
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