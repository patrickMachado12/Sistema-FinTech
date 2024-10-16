using System.ComponentModel.DataAnnotations;

namespace FinTech.Api.Damain.Models
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "O campo de E-mail é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo de Senha é obrigatório.")]
        public string Senha { get; set; }
        [Required]
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }
        [Required]
        [Range(0, 1)]
        public Boolean Ativo { get; set; }
    }
}