using Xunit;
using Moq;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.Pessoa;
using AutoMapper;

namespace FinTech.Test.TesteUnitario.Services
{
    public class PessoaServiceTests
    {
        private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PessoaService _pessoaService;

        public PessoaServiceTests()
        {
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _mapperMock = new Mock<IMapper>();
            _pessoaService = new PessoaService(_pessoaRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Deve realizar o cadastro de uma nova pessoa.")]
        public async Task Adicionar_DeveRetornarPessoaResponseContract_QuandoPessoaEValida()
        {
            // Arrange
            var pessoaRequestContract = new PessoaRequestContract { Nome = "João"};
            var pessoa = new Pessoa { Id = 1, Nome = "João"};
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João"};

            _mapperMock.Setup(m => m.Map<Pessoa>(pessoaRequestContract)).Returns(pessoa);
            _pessoaRepositoryMock.Setup(r => r.Adicionar(pessoa)).Returns(Task.FromResult(pessoa));
            _mapperMock.Setup(m => m.Map<PessoaResponseContract>(pessoa)).Returns(pessoaResponseContract);

            // Act
            var resultado = await _pessoaService.Adicionar(pessoaRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContract.Id, resultado.Id);
            Assert.Equal(pessoaResponseContract.Nome, resultado.Nome);
        }

        [Fact(DisplayName = "Deve atualizar uma pessoa existente.")]
        public async Task Atualizar_DeveRetornarPessoaResponseContract_QuandoPessoaEhValida()
        {
            // Arrange
            var pessoaRequestContract = new PessoaRequestContract { Nome = "João"};
            var pessoa = new Pessoa { Id = 1, Nome = "João"};
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João"};

            _mapperMock.Setup(m => m.Map<Pessoa>(pessoaRequestContract)).Returns(pessoa);
            _pessoaRepositoryMock.Setup(r => r.Atualizar(pessoa)).Returns(Task.FromResult(pessoa));
            _mapperMock.Setup(m => m.Map<PessoaResponseContract>(pessoa)).Returns(pessoaResponseContract);

            // Act
            var resultado = await _pessoaService.Atualizar(1, pessoaRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContract.Id, resultado.Id);
            Assert.Equal(pessoaResponseContract.Nome, resultado.Nome);
        }

        [Fact(DisplayName = "Deve deletar uma pessoa existente.")]
        public async Task Deletar_DeveRetornarPessoaResponseContract_QuandoPessoaEhValida()
        {
            // Arrange
            var id = 1;
            var pessoa = new Pessoa { Id = 1, Nome = "João"};
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João"};
            var pessoaRequestContract = new PessoaRequestContract { Nome = "João" };

            _pessoaRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(Task.FromResult(pessoa));
            _pessoaRepositoryMock.Setup(r => r.Deletar(pessoa)).Returns(Task.FromResult(true));
            _mapperMock.Setup(m => m.Map<PessoaResponseContract>(pessoa)).Returns(pessoaResponseContract);

            // Act
            var resultado = await _pessoaService.Deletar(1, pessoaResponseContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContract.Id, resultado.Id);
            Assert.Equal(pessoaResponseContract.Nome, resultado.Nome);
        }

        [Fact(DisplayName = "Deve obter uma pessoa por ID.")]
        public async Task ObterPorId_DeveRetornarPessoaResponseContract_QuandoPessoaEhValida()
        {
            // Arrange
            var id = 1;
            var pessoa = new Pessoa { Id = 1, Nome = "João"};
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João"};

            _pessoaRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(Task.FromResult(pessoa));
            _mapperMock.Setup(m => m.Map<PessoaResponseContract>(pessoa)).Returns(pessoaResponseContract);

            // Act
            var resultado = await _pessoaService.ObterPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContract.Id, resultado.Id);
            Assert.Equal(pessoaResponseContract.Nome, resultado.Nome);
        }

        [Fact(DisplayName = "Deve retornar uma lista de pessoas.")]
        public async Task ObterTodos_DeveRetornarListaDePessoaResponseContract_QuandoPessoasExistem()
        {
            // Arrange
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João"},
                new Pessoa { Id = 2, Nome = "Maria"}
            };
            var pessoaResponseContracts = new List<PessoaResponseContract>
            {
                new PessoaResponseContract { Id = 1, Nome = "João"},
                new PessoaResponseContract { Id = 2, Nome = "Maria"}
            };

            _pessoaRepositoryMock.Setup(r => r.ObterTodos()).Returns(Task.FromResult(pessoas));
            _mapperMock.Setup(m => m.Map<List<PessoaResponseContract>>(pessoas)).Returns(pessoaResponseContracts);

            // Act
            var resultado = await _pessoaService.ObterTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContracts.Count, resultado.Count());
            Assert.Equal(pessoaResponseContracts[0].Id, resultado[0].Id);
            Assert.Equal(pessoaResponseContracts[0].Nome, resultado[0].Nome);
            Assert.Equal(pessoaResponseContracts[1].Id, resultado[1].Id);
            Assert.Equal(pessoaResponseContracts[1].Nome, resultado[1].Nome);
        }
    }
}