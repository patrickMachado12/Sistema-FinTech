using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinTech.Api.Contract.Pessoa
{
    public class PessoaRequestContract
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        
    }
}