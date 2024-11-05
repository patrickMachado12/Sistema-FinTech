using System.Threading.Tasks;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Services.Classes;
using Xunit;

namespace FinTech.Test.DataBase
{
    public class APagarServiceIntegrationTests : BaseTest
    {
        [Fact]
        public async Task Deve_Criar_APagar_Com_Sucesso()
        {
            var titulo = new APagar { ValorAPagar = 100.50, Descricao = "Teste Título a Pagar" };
            var aPagarRequestContract = _mapper.Map<APagarRequestContract>(titulo);
            var resultado = await _aPagarService.Adicionar(aPagarRequestContract, 1);
            Assert.NotNull(resultado);
            Assert.Equal(100.50, resultado.ValorAPagar);
        }

        // Demais testes semelhantes aos de `PessoaServiceIntegrationTests`
    }
}
