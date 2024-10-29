using Xunit;
using Moq;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.APagar;
using AutoMapper;

namespace FinTech.Test.TesteUnitario.Services
{
    public class APagarServiceTests
    {
        private readonly Mock<IApagarRepository> _apagarRepositoryMock;
        private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly APagarService _apagarService;

        public APagarServiceTests()
        {
            _apagarRepositoryMock = new Mock<IApagarRepository>();
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _apagarService = new APagarService(_apagarRepositoryMock.Object, _pessoaRepositoryMock.Object, _usuarioRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Deve realizar o cadastro de um novo a pagar.")]
        public void Adicionar_DeveRetornarAPagarResponseContract_QuandoAPagarEhValido()
        {
            // Arrange
            var aPagarRequestContract = new APagarRequestContract { IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };

            _mapperMock.Setup(m => m.Map<APagar>(aPagarRequestContract)).Returns(aPagar);
            _apagarRepositoryMock.Setup(r => r.Adicionar(aPagar)).Returns(aPagar);
            _mapperMock.Setup(m => m.Map<APagarResponseContract>(aPagar)).Returns(aPagarResponseContract);

            // Act
            var resultado = _apagarService.Adicionar(aPagarRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Id, resultado.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, resultado.ValorAPagar);
        }

        [Fact(DisplayName = "Deve atualizar um a pagar existente.")]
        public void Atualizar_DeveRetornarAPagarResponseContract_QuandoAPagarEhValido()
        {
            // Arrange
            var aPagarRequestContract = new APagarRequestContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };

            _mapperMock.Setup(m => m.Map<APagar>(aPagarRequestContract)).Returns(aPagar);
            _apagarRepositoryMock.Setup(r => r.Atualizar(aPagar)).Returns(aPagar);
            _mapperMock.Setup(m => m.Map<APagarResponseContract>(aPagar)).Returns(aPagarResponseContract);

            // Act
            var resultado = _apagarService.Atualizar(aPagarRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Id, resultado.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, resultado.ValorAPagar);
        }

                [Fact(DisplayName = "Deve deletar um a pagar existente.")]
        public void Deletar_DeveRetornarAPagarResponseContract_QuandoAPagarEhValido()
        {
            // Arrange
            var id = 1;
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };

            _apagarRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(aPagar);
            _apagarRepositoryMock.Setup(r => r.Deletar(aPagar)).Returns(true);
            _mapperMock.Setup(m => m.Map<APagarResponseContract>(aPagar)).Returns(aPagarResponseContract);

            // Act
            var resultado = _apagarService.Deletar(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Id, resultado.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, resultado.ValorAPagar);
        }

        [Fact(DisplayName = "Deve obter um a pagar por ID.")]
        public void ObterPorId_DeveRetornarAPagarResponseContract_QuandoAPagarEhValido()
        {
            // Arrange
            var id = 1;
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };

            _apagarRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(aPagar);
            _mapperMock.Setup(m => m.Map<APagarResponseContract>(aPagar)).Returns(aPagarResponseContract);

            // Act
            var resultado = _apagarService.ObterPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Id, resultado.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, resultado.ValorAPagar);
        }

        [Fact(DisplayName = "Deve retornar uma lista de a pagar.")]
        public void ObterTodos_DeveRetornarListaDeAPagarResponseContract_QuandoAPagarExistem()
        {
            // Arrange
            var aPagar = new List<APagar>
            {
                new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m },
                new APagar { Id = 2, IdPessoa = 2, IdNaturezaLancamento = 2, ValorAPagar = 200.00m }
            };
            var aPagarResponseContract = new List<APagarResponseContract>
            {
                new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m },
                new APagarResponseContract { Id = 2, IdPessoa = 2, IdNaturezaLancamento = 2, ValorAPagar = 200.00m }
            };

            _apagarRepositoryMock.Setup(r => r.ObterTodos()).Returns(aPagar);
            _mapperMock.Setup(m => m.Map<List<APagarResponseContract>>(aPagar)).Returns(aPagarResponseContract);

            // Act
            var resultado = _apagarService.ObterTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aPagarResponseContract.Count, resultado.Count);
            Assert.Equal(aPagarResponseContract[0].Id, resultado[0].Id);
            Assert.Equal(aPagarResponseContract[0].IdPessoa, resultado[0].IdPessoa);
            Assert.Equal(aPagarResponseContract[0].IdNaturezaLancamento, resultado[0].IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract[0].ValorAPagar, resultado[0].ValorAPagar);
            Assert.Equal(aPagarResponseContract[1].Id, resultado[1].Id);
            Assert.Equal(aPagarResponseContract[1].IdPessoa, resultado[1].IdPessoa);
            Assert.Equal(aPagarResponseContract[1].IdNaturezaLancamento, resultado[1].IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract[1].ValorAPagar, resultado[1].ValorAPagar);
        }
    }
}
       