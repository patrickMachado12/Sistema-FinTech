using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Domain.Models;
using Xunit;

namespace FinTech.Test.DataBase
{
    public class APagarServiceIntegrationTests : BaseTest
    {
        [Fact]
        public async Task Deve_Criar_APagar_Com_Sucesso()
        {
            var naturezaLancamento = new NaturezaLancamento { Descricao = "Natureza de Lançamento Teste" };

            var naturezaLancamentoRequestContract = _mapper.Map<NaturezaLancamentoRequestContract>(naturezaLancamento);
            var naturezaLancamentoCriada = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, 1);

            var titulo = new APagar
            {
                IdNaturezaLancamento = naturezaLancamentoCriada.Id,
                ValorAPagar = 100.50,
                Descricao = "Teste Título a Pagar",
                Observacao = "Teste Observação",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
            };

            var aPagarRequestContract = _mapper.Map<APagarRequestContract>(titulo);

            var resultado = await _aPagarService.Adicionar(aPagarRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.True(resultado.Id > 0, "O Id do título a Pagar deve ser maior que 0.");
            Assert.Equal(100.50, resultado.ValorAPagar);
            Assert.Equal("Teste Título a Pagar", resultado.Descricao);
            Assert.Equal(naturezaLancamentoCriada.Id, resultado.IdNaturezaLancamento);
            Assert.Equal("Teste Observação", resultado.Observacao);
            Assert.InRange(resultado.DataEmissao, DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1));
            Assert.InRange(resultado.DataVencimento, DateTime.Now.AddDays(29), DateTime.Now.AddDays(31));
        }

        [Fact]
        public async Task Deve_Obtereber_Por_Id_Com_Sucesso()
        {
            var naturezaLancamento = new NaturezaLancamento { Descricao = "Natureza de Lançamento Teste" };

            var naturezaLancamentoRequestContract = _mapper.Map<NaturezaLancamentoRequestContract>(naturezaLancamento);
            var naturezaLancamentoCriada = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, 1);

            var titulo = new APagar
            {
                IdNaturezaLancamento = naturezaLancamentoCriada.Id,
                ValorAPagar = 100.50,
                Descricao = "Teste Título a Pagar",
                Observacao = "Teste Observação",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
            };

            var aPagarRequestContract = _mapper.Map<APagarRequestContract>(titulo);
            var resultadoCriacao = await _aPagarService.Adicionar(aPagarRequestContract, 1);

            var resultadoLeitura = await _aPagarService.Obter(resultadoCriacao.Id, 1);

            Assert.NotNull(resultadoLeitura);
            Assert.Equal(resultadoCriacao.Id, resultadoLeitura.Id);
            Assert.Equal("Teste Título a Pagar", resultadoLeitura.Descricao);
            Assert.Equal(100.50, resultadoLeitura.ValorAPagar);
        }

        [Fact]
        public async Task Deve_Atualizar_APagar_Com_Sucesso()
        {
            var naturezaLancamento = new NaturezaLancamento { Descricao = "Natureza de Lançamento Teste" };

            var naturezaLancamentoRequestContract = _mapper.Map<NaturezaLancamentoRequestContract>(naturezaLancamento);
            var usuarioCriado = await _usuarioRepository.ObterPorId(1);
            var naturezaLancamentoCriada = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, usuarioCriado.Id);  

            var titulo = new APagar
            {
                IdNaturezaLancamento = naturezaLancamentoCriada.Id,
                ValorAPagar = 100.50,
                Descricao = "Teste Título a Pagar",
                Observacao = "Teste Observação",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
            };

            var aPagarRequestContract = _mapper.Map<APagarRequestContract>(titulo);
            var resultadoCriacao = await _aPagarService.Adicionar(aPagarRequestContract, usuarioCriado.Id);  

            resultadoCriacao.Descricao = "Título Atualizado";
            var aPagarAtualizado = _mapper.Map<APagarRequestContract>(resultadoCriacao);
            var resultadoAtualizacao = await _aPagarService.Atualizar(resultadoCriacao.Id, aPagarAtualizado, usuarioCriado.Id);  

            Assert.NotNull(resultadoAtualizacao);
            Assert.Equal("Título Atualizado", resultadoAtualizacao.Descricao);
        }
                
        [Fact]
        public async Task Deve_Excluir_APagar_Com_Sucesso()
        {
            var naturezaLancamento = new NaturezaLancamento { Descricao = "Natureza de Lançamento Teste" };

            var naturezaLancamentoRequestContract = _mapper.Map<NaturezaLancamentoRequestContract>(naturezaLancamento);
            var naturezaLancamentoCriada = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, 1);

            var titulo = new APagar
            {
                IdNaturezaLancamento = naturezaLancamentoCriada.Id,
                ValorAPagar = 100.50,
                Descricao = "Teste Título a Pagar",
                Observacao = "Teste Observação",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
            };

            var aPagarRequestContract = _mapper.Map<APagarRequestContract>(titulo);
            var resultadoCriacao = await _aPagarService.Adicionar(aPagarRequestContract, 1);

            await _aPagarService.Inativar(resultadoCriacao.Id, 1);
            var resultadoExclusao = await _aPagarService.Obter(resultadoCriacao.Id, 1);

            Assert.Null(resultadoExclusao);
        }

    }
}
