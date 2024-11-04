namespace FinTech.Api.Contract.NaturezaLancamento
{
    public class NaturezaLancamentoResponseContract : NaturezaLancamentoRequestContract
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}