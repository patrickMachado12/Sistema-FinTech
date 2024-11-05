using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FinTech.Api.Contract.AReceber;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Contract.Pessoa;
using FinTech.Api.Domain.Models;
using Xunit;

namespace FinTech.Test.DataBase
{
    public class AReceberServiceIntegrationTests : BaseTest
    {
        [Fact]
        public async Task Deve_Criar_AReceber_Com_Sucesso()
        {
            var pessoa = new Pessoa { Nome = "Pessoa Teste", Telefone = "12345678901" };
            var naturezaLancamento = new NaturezaLancamento { Descricao = "Natureza de Lançamento Teste" };

            var pessoaRequestContract = _mapper.Map<PessoaRequestContract>(pessoa);
            var pessoaCriada = await _pessoaService.Adicionar(pessoaRequestContract);
            var naturezaLancamentoRequestContract = _mapper.Map<NaturezaLancamentoRequestContract>(naturezaLancamento);
            var naturezaLancamentoCriada = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, 1);

            var titulo = new AReceber
            {
                IdPessoa = pessoaCriada.Id,
                IdNaturezaLancamento = naturezaLancamentoCriada.Id,
                ValorAReceber = 100.50,
                Descricao = "Teste Título a Receber",
                Observacao = "Teste Observação",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
                DataReferencia = DateTime.Now
            };

            var aReceberRequestContract = _mapper.Map<AReceberRequestContract>(titulo);

            var resultado = await _aReceberService.Adicionar(aReceberRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.True(resultado.Id > 0, "O Id do título a receber deve ser maior que 0.");
            Assert.Equal(100.50, resultado.ValorAReceber);
            Assert.Equal("Teste Título a Receber", resultado.Descricao);
            Assert.Equal(pessoaCriada.Id, resultado.IdPessoa);
            Assert.Equal(naturezaLancamentoCriada.Id, resultado.IdNaturezaLancamento);
            Assert.Equal("Teste Observação", resultado.Observacao);
            Assert.InRange(resultado.DataEmissao, DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1));
            Assert.InRange(resultado.DataVencimento, DateTime.Now.AddDays(29), DateTime.Now.AddDays(31));
        }

        [Fact]
        public async Task Deve_Obtereber_Por_Id_Com_Sucesso()
        {
            var pessoa = new Pessoa { Nome = "Pessoa Teste", Telefone = "12345678901" };
            var naturezaLancamento = new NaturezaLancamento { Descricao = "Natureza de Lançamento Teste" };

            var pessoaRequestContract = _mapper.Map<PessoaRequestContract>(pessoa);
            var pessoaCriada = await _pessoaService.Adicionar(pessoaRequestContract);
            var naturezaLancamentoRequestContract = _mapper.Map<NaturezaLancamentoRequestContract>(naturezaLancamento);
            var naturezaLancamentoCriada = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, 1);

            var titulo = new AReceber
            {
                IdPessoa = pessoaCriada.Id,
                IdNaturezaLancamento = naturezaLancamentoCriada.Id,
                ValorAReceber = 100.50,
                Descricao = "Teste Título a Receber",
                Observacao = "Teste Observação",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
                DataReferencia = DateTime.Now
            };

            var aReceberRequestContract = _mapper.Map<AReceberRequestContract>(titulo);
            var resultadoCriacao = await _aReceberService.Adicionar(aReceberRequestContract, 1);

            var resultadoLeitura = await _aReceberService.Obter(resultadoCriacao.Id, 1);

            Assert.NotNull(resultadoLeitura);
            Assert.Equal(resultadoCriacao.Id, resultadoLeitura.Id);
            Assert.Equal("Teste Título a Receber", resultadoLeitura.Descricao);
            Assert.Equal(100.50, resultadoLeitura.ValorAReceber);
        }


        [Fact]
        public async Task Deve_Atualizar_AReceber_Com_Sucesso()
        {
            var pessoa = new Pessoa { Nome = "Pessoa Teste", Telefone = "12345678901" };
            var naturezaLancamento = new NaturezaLancamento { Descricao = "Natureza de Lançamento Teste" };

            var pessoaRequestContract = _mapper.Map<PessoaRequestContract>(pessoa);
            var pessoaCriada = await _pessoaService.Adicionar(pessoaRequestContract);  

            var naturezaLancamentoRequestContract = _mapper.Map<NaturezaLancamentoRequestContract>(naturezaLancamento);
            var usuarioCriado = await _usuarioRepository.ObterPorId(1);
            var naturezaLancamentoCriada = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, usuarioCriado.Id);  

            var titulo = new AReceber
            {
                IdPessoa = pessoaCriada.Id,
                IdNaturezaLancamento = naturezaLancamentoCriada.Id,
                ValorAReceber = 100.50,
                Descricao = "Teste Título a Receber",
                Observacao = "Teste Observação",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
                DataReferencia = DateTime.Now
            };

            var aReceberRequestContract = _mapper.Map<AReceberRequestContract>(titulo);
            var resultadoCriacao = await _aReceberService.Adicionar(aReceberRequestContract, usuarioCriado.Id);  

            resultadoCriacao.Descricao = "Título Atualizado";
            var aReceberAtualizado = _mapper.Map<AReceberRequestContract>(resultadoCriacao);
            var resultadoAtualizacao = await _aReceberService.Atualizar(resultadoCriacao.Id, aReceberAtualizado, usuarioCriado.Id);  

            Assert.NotNull(resultadoAtualizacao);
            Assert.Equal("Título Atualizado", resultadoAtualizacao.Descricao);
        }
                
        [Fact]
        public async Task Deve_Excluir_AReceber_Com_Sucesso()
        {
            var pessoa = new Pessoa { Nome = "Pessoa Teste", Telefone = "12345678901" };
            var naturezaLancamento = new NaturezaLancamento { Descricao = "Natureza de Lançamento Teste" };

            var pessoaRequestContract = _mapper.Map<PessoaRequestContract>(pessoa);
            var pessoaCriada = await _pessoaService.Adicionar(pessoaRequestContract);
            var naturezaLancamentoRequestContract = _mapper.Map<NaturezaLancamentoRequestContract>(naturezaLancamento);
            var naturezaLancamentoCriada = await _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract, 1);

            var titulo = new AReceber
            {
                IdPessoa = pessoaCriada.Id,
                IdNaturezaLancamento = naturezaLancamentoCriada.Id,
                ValorAReceber = 100.50,
                Descricao = "Teste Título a Receber",
                Observacao = "Teste Observação",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now.AddDays(30),
                DataReferencia = DateTime.Now
            };

            var aReceberRequestContract = _mapper.Map<AReceberRequestContract>(titulo);
            var resultadoCriacao = await _aReceberService.Adicionar(aReceberRequestContract, 1);

            await _aReceberService.Inativar(resultadoCriacao.Id, 1);
            var resultadoExclusao = await _aReceberService.Obter(resultadoCriacao.Id, 1);

            Assert.Null(resultadoExclusao);
        }

    }
}
