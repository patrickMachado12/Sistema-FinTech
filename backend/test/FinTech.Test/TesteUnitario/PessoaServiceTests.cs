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
        public void Adicionar_DeveRetornarPessoaResponseContract_QuandoPessoaEhValida()
        {
            // Arrange
            var pessoaRequestContract = new PessoaRequestContract { Nome = "João", Email = "joao@email.com" };
            var pessoa = new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _mapperMock.Setup(m => m.Map<Pessoa>(pessoaRequestContract)).Returns(pessoa);
            _pessoaRepositoryMock.Setup(r => r.Adicionar(pessoa)).Returns(pessoa);
            _mapperMock.Setup(m => m.Map<PessoaResponseContract>(pessoa)).Returns(pessoaResponseContract);

            // Act
            var resultado = _pessoaService.Adicionar(pessoaRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContract.Id, resultado.Id);
            Assert.Equal(pessoaResponseContract.Nome, resultado.Nome);
            Assert.Equal(pessoaResponseContract.Email, resultado.Email);
        }

        [Fact(DisplayName = "Deve atualizar uma pessoa existente.")]
        public void Atualizar_DeveRetornarPessoaResponseContract_QuandoPessoaEhValida()
        {
            // Arrange
            var pessoaRequestContract = new PessoaRequestContract { Id = 1, Nome = "João", Email = "joao@email.com" };
            var pessoa = new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _mapperMock.Setup(m => m.Map<Pessoa>(pessoaRequestContract)).Returns(pessoa);
            _pessoaRepositoryMock.Setup(r => r.Atualizar(pessoa)).Returns(pessoa);
            _mapperMock.Setup(m => m.Map<PessoaResponseContract>(pessoa)).Returns(pessoaResponseContract);

            // Act
            var resultado = _pessoaService.Atualizar(pessoaRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContract.Id, resultado.Id);
            Assert.Equal(pessoaResponseContract.Nome, resultado.Nome);
            Assert.Equal(pessoaResponseContract.Email, resultado.Email);
        }

        [Fact(DisplayName = "Deve deletar uma pessoa existente.")]
        public void Deletar_DeveRetornarPessoaResponseContract_QuandoPessoaEhValida()
        {
            // Arrange
            var id = 1;
            var pessoa = new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _pessoaRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(pessoa);
            _pessoaRepositoryMock.Setup(r => r.Deletar(pessoa)).Returns(true);
            _mapperMock.Setup(m => m.Map<PessoaResponseContract>(pessoa)).Returns(pessoaResponseContract);

            // Act
            var resultado = _pessoaService.Deletar(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContract.Id, resultado.Id);
            Assert.Equal(pessoaResponseContract.Nome, resultado.Nome);
            Assert.Equal(pessoaResponseContract.Email, resultado.Email);
        }

        [Fact(DisplayName = "Deve obter uma pessoa por ID.")]
        public void ObterPorId_DeveRetornarPessoaResponseContract_QuandoPessoaEhValida()
        {
            // Arrange
            var id = 1;
            var pessoa = new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _pessoaRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(pessoa);
            _mapperMock.Setup(m => m.Map<PessoaResponseContract>(pessoa)).Returns(pessoaResponseContract);

            // Act
            var resultado = _pessoaService.ObterPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContract.Id, resultado.Id);
            Assert.Equal(pessoaResponseContract.Nome, resultado.Nome);
            Assert.Equal(pessoaResponseContract.Email, resultado.Email);
        }

        [Fact(DisplayName = "Deve retornar uma lista de pessoas.")]
        public void ObterTodos_DeveRetornarListaDePessoaResponseContract_QuandoPessoasExistem()
        {
            // Arrange
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" },
                new Pessoa { Id = 2, Nome = "Maria", Email = "maria@email.com" }
            };
            var pessoaResponseContracts = new List<PessoaResponseContract>
            {
                new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" },
                new PessoaResponseContract { Id = 2, Nome = "Maria", Email = "maria@email.com" }
            };

            _pessoaRepositoryMock.Setup(r => r.ObterTodos()).Returns(pessoas);
            _mapperMock.Setup(m => m.Map<List<PessoaResponseContract>>(pessoas)).Returns(pessoaResponseContracts);

            // Act
            var resultado = _pessoaService.ObterTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(pessoaResponseContracts.Count, resultado.Count);
            Assert.Equal(pessoaResponseContracts[0].Id, resultado[0].Id);
            Assert.Equal(pessoaResponseContracts[0].Nome, resultado[0].Nome);
            Assert.Equal(pessoaResponseContracts[0].Email, resultado[0].Email);
            Assert.Equal(pessoaResponseContracts[1].Id, resultado[1].Id);
            Assert.Equal(pessoaResponseContracts[1].Nome, resultado[1].Nome);
            Assert.Equal(pessoaResponseContracts[1].Email, resultado[1].Email);
        }
    }
}