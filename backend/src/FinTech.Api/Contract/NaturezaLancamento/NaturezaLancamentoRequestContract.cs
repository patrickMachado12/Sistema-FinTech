using System.ComponentModel.DataAnnotations;

namespace FinTech.Api.Contract.NaturezaLancamento
{
    public class NaturezaLancamentoRequestContract
    {
        [Required(ErrorMessage = "O campo Descricao é obrigatório.")]
        public string Descricao { get; set; }
        public string Observacao { get; set; } = string.Empty;
    }
}