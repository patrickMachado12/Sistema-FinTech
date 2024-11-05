using System.Threading.Tasks;
using FinTech.Api.Contract.AReceber;
using FinTech.Api.Domain.Models;
using Xunit;

namespace FinTech.Test.DataBase
{
    public class AReceberServiceIntegrationTests : BaseTest
    {
        [Fact]
        public async Task Deve_Criar_AReceber_Com_Sucesso()
        {
            var titulo = new AReceber { ValorAReceber = 100.50, Descricao = "Teste Título a Receber" };
            var aReceberRequestContract = _mapper.Map<AReceberRequestContract>(titulo);
            var resultado = await _aReceberService.Adicionar(aReceberRequestContract, 1);
            Assert.NotNull(resultado);
            Assert.Equal(100.50, resultado.ValorAReceber);
        }

        // Demais testes semelhantes aos de `PessoaServiceIntegrationTests`
    }
}
