using System.Threading.Tasks;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Domain.Models;
using Xunit;

namespace FinTech.Test.DataBase
{
    public class NaturezaLancamentoServiceIntegrationTests : BaseTest
    {
        [Fact]
        public async Task Deve_Criar_NaturezaLancamento_Com_Sucesso()
        {
            var naturezaRequest = new NaturezaLancamentoRequestContract { Descricao = "Teste de Lançamento" };
            var resultado = await _naturezaLancamentoService.Adicionar(naturezaRequest, 1);
            
            Assert.NotNull(resultado);
            Assert.Equal("Teste de Lançamento", resultado.Descricao);
        }

        [Fact]
        public async Task Deve_Obter_NaturezaLancamento_Por_Id()
        {
            var naturezaRequest = new NaturezaLancamentoRequestContract { Descricao = "Teste de Lançamento" };
            var natureza = await _naturezaLancamentoService.Adicionar(naturezaRequest, 1);

            var resultado = await _naturezaLancamentoService.Obter(natureza.Id, 1);

            Assert.NotNull(resultado);
            Assert.Equal(natureza.Id, resultado.Id);
            Assert.Equal(natureza.Descricao, resultado.Descricao);
        }

        [Fact]
        public async Task Deve_Atualizar_NaturezaLancamento_Com_Sucesso()
        {
            var naturezaRequest = new NaturezaLancamentoRequestContract { Descricao = "Teste de Lançamento" };
            var natureza = await _naturezaLancamentoService.Adicionar(naturezaRequest, 1);
            var naturezaAtualizadaRequest = new NaturezaLancamentoRequestContract { Descricao = "Lançamento Atualizado" };

            var resultado = await _naturezaLancamentoService.Atualizar(natureza.Id, naturezaAtualizadaRequest, 1);

            Assert.NotNull(resultado);
            Assert.Equal("Lançamento Atualizado", resultado.Descricao);
        }

        [Fact]
        public async Task Deve_Deletar_NaturezaLancamento_Com_Sucesso()
        {
            var naturezaRequest = new NaturezaLancamentoRequestContract { Descricao = "Teste de Lançamento" };
            var natureza = await _naturezaLancamentoService.Adicionar(naturezaRequest, 1);

            await _naturezaLancamentoService.Inativar(natureza.Id, 1);
            var resultado = await _naturezaLancamentoService.Obter(natureza.Id, 1);

            Assert.Null(resultado);
        }
    }
}
