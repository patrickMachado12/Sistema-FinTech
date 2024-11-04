namespace FinTech.Api.Contract.APagar
{
    public class APagarResponseContract : APagarRequestContract
    {
        public long Id { get; set; }
        // public long IdPessoa { get; set; }
        public PessoaResponse Pessoa { get; set; }
        // public long IdNaturezaLancamento { get; set; }
        public double ValorAPagar { get; set; }
        public double ValorPago { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public NaturezaLancamentoResponse NaturezaLancamento { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataReferencia { get; set; }        
    }

    public class PessoaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class NaturezaLancamentoResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}