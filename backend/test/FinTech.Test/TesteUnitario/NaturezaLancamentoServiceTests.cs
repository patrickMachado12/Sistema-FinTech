using Xunit;
using Moq;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.NaturezaLancamento;
using AutoMapper;

namespace FinTech.Test.TesteUnitario.Services
{
    public class NaturezaLancamentoServiceTests
    {
        private readonly Mock<INaturezaLancamentoRepository> _naturezaLancamentoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly NaturezaLancamentoService _naturezaLancamentoService;

        public NaturezaLancamentoServiceTests()
        {
            _naturezaLancamentoRepositoryMock = new Mock<INaturezaLancamentoRepository>();
            _mapperMock = new Mock<IMapper>();
            _naturezaLancamentoService = new NaturezaLancamentoService(_naturezaLancamentoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Deve realizar o cadastro de uma nova natureza de lançamento.")]
        public void Adicionar_DeveRetornarNaturezaLancamentoResponseContract_QuandoNaturezaLancamentoEhValida()
        {
            // Arrange
            var naturezaLancamentoRequestContract = new NaturezaLancamentoRequestContract { Descricao = "Lançamento de teste" };
            var naturezaLancamento = new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste" };

            _mapperMock.Setup(m => m.Map<NaturezaLancamento>(naturezaLancamentoRequestContract)).Returns(naturezaLancamento);
            _naturezaLancamentoRepositoryMock.Setup(r => r.Adicionar(naturezaLancamento)).Returns(naturezaLancamento);
            _mapperMock.Setup(m => m.Map<NaturezaLancamentoResponseContract>(naturezaLancamento)).Returns(naturezaLancamentoResponseContract);

            // Act
            var resultado = _naturezaLancamentoService.Adicionar(naturezaLancamentoRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(naturezaLancamentoResponseContract.Id, resultado.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, resultado.Descricao);
        }

        [Fact(DisplayName = "Deve atualizar uma natureza de lançamento existente.")]
        public void Atualizar_DeveRetornarNaturezaLancamentoResponseContract_QuandoNaturezaLancamentoEhValida()
        {
            // Arrange
            var naturezaLancamentoRequestContract = new NaturezaLancamentoRequestContract { Id = 1, Descricao = "Lançamento de teste atualizado" };
            var naturezaLancamento = new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste atualizado" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste atualizado" };

            _mapperMock.Setup(m => m.Map<NaturezaLancamento>(naturezaLancamentoRequestContract)).Returns(naturezaLancamento);
            _naturezaLancamentoRepositoryMock.Setup(r => r.Atualizar(naturezaLancamento)).Returns(naturezaLancamento);
            _mapperMock.Setup(m => m.Map<NaturezaLancamentoResponseContract>(naturezaLancamento)).Returns(naturezaLancamentoResponseContract);

            // Act
            var resultado = _naturezaLancamentoService.Atualizar(naturezaLancamentoRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(naturezaLancamentoResponseContract.Id, resultado.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, resultado.Descricao);
        }

        [Fact(DisplayName = "Deve deletar uma natureza de lançamento existente.")]
        public void Deletar_DeveRetornarNaturezaLancamentoResponseContract_QuandoNaturezaLancamentoEhValida()
        {
            // Arrange
            var id = 1;
            var naturezaLancamento = new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste" };

            _naturezaLancamentoRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(naturezaLancamento);
            _naturezaLancamentoRepositoryMock.Setup(r => r.Deletar(naturezaLancamento)).Returns(true);
            _mapperMock.Setup(m => m.Map<NaturezaLancamentoResponseContract>(naturezaLancamento)).Returns(naturezaLancamentoResponseContract);

            // Act
            var resultado = _naturezaLancamentoService.Deletar(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(naturezaLancamentoResponseContract.Id, resultado.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, resultado.Descricao);
        }

        [Fact(DisplayName = "Deve obter uma natureza de lançamento por ID.")]
        public void ObterPorId_DeveRetornarNaturezaLancamentoResponseContract_QuandoNaturezaLancamentoEhValida()
        {
            // Arrange
            var id = 1;
            var naturezaLancamento = new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste" };

            _naturezaLancamentoRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(naturezaLancamento);
            _mapperMock.Setup(m => m.Map<NaturezaLancamentoResponseContract>(naturezaLancamento)).Returns(naturezaLancamentoResponseContract);

            // Act
            var resultado = _naturezaLancamentoService.ObterPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(naturezaLancamentoResponseContract.Id, resultado.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, resultado.Descricao);
        }

        [Fact(DisplayName = "Deve retornar uma lista de naturezas de lançamento.")]
        public void ObterTodos_DeveRetornarListaDeNaturezaLancamentoResponseContract_QuandoNaturezasLancamentoExistem()
        {
            // Arrange
            var naturezasLancamento = new List<NaturezaLancamento>
            {
                new NaturezaLancamento { Id = 1, Descricao = "Lançamento de teste" },
                new NaturezaLancamento { Id = 2, Descricao = "Lançamento de teste 2" }
            };
            var naturezasLancamentoResponseContracts = new List<NaturezaLancamentoResponseContract>
            {
                new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Lançamento de teste" },
                new NaturezaLancamentoResponseContract { Id = 2, Descricao = "Lançamento de teste 2" }
            };

            _naturezaLancamentoRepositoryMock.Setup(r => r.ObterTodos()).Returns(naturezasLancamento);
            _mapperMock.Setup(m => m.Map<List<NaturezaLancamentoResponseContract>>(naturezasLancamento)).Returns(naturezasLancamentoResponseContracts);

            // Act
            var resultado = _naturezaLancamentoService.ObterTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(naturezasLancamentoResponseContracts.Count, resultado.Count);
            Assert.Equal(naturezasLancamentoResponseContracts[0].Id, resultado[0].Id);
            Assert.Equal(naturezasLancamentoResponseContracts[0].Descricao, resultado[0].Descricao);
            Assert.Equal(naturezasLancamentoResponseContracts[1].Id, resultado[1].Id);
            Assert.Equal(naturezasLancamentoResponseContracts[1].Descricao, resultado[1].Descricao);
        }
    }
}