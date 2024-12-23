using System.ComponentModel.DataAnnotations;

namespace FinTech.Api.Domain.Models
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo de E-mail é obrigatório.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O campo de Senha é obrigatório.")]
        public string Senha { get; set; } = null!;

        [Required]
        public DateTime DataCadastro { get; set; }

        public DateTime? DataInativacao { get; set; }
        
    }
}