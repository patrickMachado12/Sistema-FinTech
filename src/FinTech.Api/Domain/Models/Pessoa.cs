using System.ComponentModel.DataAnnotations;

namespace FinTech.Api.Domain.Models
{
    public class Pessoa
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        [Required]
        [Range(0, 1)]
        public Boolean Status { get; set; }
    }
}