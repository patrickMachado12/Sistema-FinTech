using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinTech.Api.Contract.NaturezaLancamento
{
    public class NaturezaLancamentoRequestContract
    {
        public string Descricao { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
    }
}