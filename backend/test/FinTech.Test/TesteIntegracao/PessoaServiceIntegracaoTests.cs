using System.Threading.Tasks;
using FinTech.Api.Contract.Pessoa;
using FinTech.Api.Domain.Models;
using Xunit;

namespace FinTech.Test.DataBase
{
    public class PessoaServiceIntegrationTests : BaseTest
    {
        [Fact]
        public async Task Deve_Criar_Pessoa_Com_Sucesso()
        {
            var pessoa = new Pessoa 
            { 
                Nome = "Pessoa Teste",
                Telefone = "123456789"
            };
            var pessoaRequestContract = _mapper.Map<PessoaRequestContract>(pessoa);

            var resultado = await _pessoaService.Adicionar(pessoaRequestContract);
            Assert.NotNull(resultado);
            Assert.Equal("Pessoa Teste", resultado.Nome);
        }

        [Fact]
        public async Task Deve_Obter_Pessoa_Por_Id()
        {
            var pessoaRequestContract = _mapper.Map<PessoaRequestContract>(new Pessoa 
            { 
                Nome = "Pessoa Teste",
                Telefone = "123456789"
            });
            var pessoa = await _pessoaService.Adicionar(pessoaRequestContract);
            var resultado = await _pessoaService.ObterPorId(pessoa.Id);
            Assert.NotNull(resultado);
            Assert.Equal(pessoa.Id, resultado.Id);
        }

        [Fact]
        public async Task Deve_Atualizar_Pessoa_Com_Sucesso()
        {
            var pessoaRequestContract = _mapper.Map<PessoaRequestContract>(new Pessoa 
            { 
                Nome = "Pessoa Teste",
                Telefone = "123456789" 
            });
            var pessoa = await _pessoaService.Adicionar(pessoaRequestContract);
            pessoaRequestContract.Nome = "Pessoa Atualizada";

            await _pessoaService.Atualizar(pessoa.Id, pessoaRequestContract);

            var resultado = await _pessoaService.ObterPorId(pessoa.Id);
            Assert.Equal("Pessoa Atualizada", resultado.Nome);
        }

        [Fact]
        public async Task Deve_Deletar_Pessoa_Com_Sucesso()
        {
            var pessoaRequestContract = _mapper.Map<PessoaRequestContract>(new Pessoa 
            { 
                Nome = "Pessoa Teste",
                Telefone = "123456789"
            });
            var pessoa = await _pessoaService.Adicionar(pessoaRequestContract);

            await _pessoaService.Deletar(pessoa.Id, pessoaRequestContract);
            
            var resultado = await _pessoaService.ObterPorId(pessoa.Id);
            Assert.Null(resultado);
        }
    }
}
