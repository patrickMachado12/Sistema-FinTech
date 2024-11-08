using System.ComponentModel.DataAnnotations;

namespace FinTech.Api.Domain.Models
{
    public class AReceber : Titulo
    {

        [Required(ErrorMessage = "O campo de Valor é obrigatório.")]
        public double ValorAReceber { get; set; }
        
        public double ValorBaixado { get; set; }

        public DateTime? DataRecebimento { get; set; }

    }
}