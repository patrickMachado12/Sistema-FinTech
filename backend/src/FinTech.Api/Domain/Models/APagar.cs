using System.ComponentModel.DataAnnotations;

namespace FinTech.Api.Domain.Models
{
    public class APagar : Titulo
    {

        [Required(ErrorMessage = "O campo de Valor é obrigatório.")]
        public double ValorAPagar { get; set; }

        public double ValorPago { get; set; }

        public DateTime? DataPagamento { get; set; }

    }
}