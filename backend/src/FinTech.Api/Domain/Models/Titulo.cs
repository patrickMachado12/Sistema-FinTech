using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinTech.Api.Domain.Models
{
    public abstract class Titulo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long IdUsuario { get; set; }

        public Usuario Usuario { get; set; } = null!;

        [Required]
        public long IdNaturezaLancamento { get; set; }

        public NaturezaLancamento NaturezaLancamento { get; set; } = null!;

        [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo DataEmissao é obrigatório.")]
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "O campo DataVencimento é obrigatório.")]
        public DateTime DataVencimento { get; set; }

        public string? Observacao { get; set; } = string.Empty;
    }
}