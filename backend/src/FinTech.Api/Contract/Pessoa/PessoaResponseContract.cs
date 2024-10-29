using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinTech.Api.Contract.Pessoa
{
    public class PessoaResponseContract : PessoaRequestContract
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}