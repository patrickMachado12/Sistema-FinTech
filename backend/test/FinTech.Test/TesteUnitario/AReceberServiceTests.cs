using Xunit;
using Moq;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.AReceber;

namespace FinTech.Test.TesteUnitario.Services
{
    public class AReceberServiceTests
    {
        private readonly Mock<IAReceberRepository> _aReceberRepositoryMock;
        private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AReceberService _aReceberService;

        public AReceberServiceTests()
        {
            _aReceberRepositoryMock = new Mock<IAReceberRepository>();
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _aReceberService = new AReceberService(_aReceberRepositoryMock.Object, _pessoaRepositoryMock.Object, _usuarioRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Deve realizar o cadastro de um novo a receber.")]
        public void Adicionar_DeveRetornarAReceberResponseContract_QuandoAReceberEhValido()
        {
            // Arrange
            var aReceberRequestContract = new AReceberRequestContract { IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceber = new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };

            _mapperMock.Setup(m => m.Map<AReceber>(aReceberRequestContract)).Returns(aReceber);
            _aReceberRepositoryMock.Setup(r => r.Adicionar(aReceber)).Returns(aReceber);
            _mapperMock.Setup(m => m.Map<AReceberResponseContract>(aReceber)).Returns(aReceberResponseContract);

            // Act
            var resultado = _aReceberService.Adicionar(aReceberRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Id, resultado.Id);
            Assert.Equal(aReceberResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, resultado.ValorAReceber);
        }

                [Fact(DisplayName = "Deve atualizar um a receber existente.")]
        public void Atualizar_DeveRetornarAReceberResponseContract_QuandoAReceberEhValido()
        {
            // Arrange
            var aReceberRequestContract = new AReceberRequestContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceber = new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };

            _mapperMock.Setup(m => m.Map<AReceber>(aReceberRequestContract)).Returns(aReceber);
            _aReceberRepositoryMock.Setup(r => r.Atualizar(aReceber)).Returns(aReceber);
            _mapperMock.Setup(m => m.Map<AReceberResponseContract>(aReceber)).Returns(aReceberResponseContract);

            // Act
            var resultado = _aReceberService.Atualizar(aReceberRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Id, resultado.Id);
            Assert.Equal(aReceberResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, resultado.ValorAReceber);
        }

        [Fact(DisplayName = "Deve deletar um a receber existente.")]
        public void Deletar_DeveRetornarAReceberResponseContract_QuandoAReceberEhValido()
        {
            // Arrange
            var id = 1;
            var aReceber = new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };

            _aReceberRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(aReceber);
            _aReceberRepositoryMock.Setup(r => r.Deletar(aReceber)).Returns(true);
            _mapperMock.Setup(m => m.Map<AReceberResponseContract>(aReceber)).Returns(aReceberResponseContract);

            // Act
            var resultado = _aReceberService.Deletar(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Id, resultado.Id);
            Assert.Equal(aReceberResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, resultado.ValorAReceber);
        }

        [Fact(DisplayName = "Deve obter um a receber por ID.")]
        public void ObterPorId_DeveRetornarAReceberResponseContract_QuandoAReceberEhValido()
        {
            // Arrange
            var id = 1;
            var aReceber = new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };

            _aReceberRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(aReceber);
            _mapperMock.Setup(m => m.Map<AReceberResponseContract>(aReceber)).Returns(aReceberResponseContract);

            // Act
            var resultado = _aReceberService.ObterPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Id, resultado.Id);
            Assert.Equal(aReceberResponseContract.IdPessoa, resultado.IdPessoa);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, resultado.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, resultado.ValorAReceber);
        }

        [Fact(DisplayName = "Deve retornar uma lista de a receber.")]
        public void ObterTodos_DeveRetornarListaDeAReceberResponseContract_QuandoAReceberExistem()
        {
            // Arrange
            var aReceber = new List<AReceber>
            {
                new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m },
                new AReceber { Id = 2, IdPessoa = 2, IdNaturezaLancamento = 2, ValorAReceber = 200.00m }
            };
            var aReceberResponseContract = new List<AReceberResponseContract>
            {
                new AReceberResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m },
                new AReceberResponseContract { Id = 2, IdPessoa = 2, IdNaturezaLancamento = 2, ValorAReceber = 200.00m }
            };

            _aReceberRepositoryMock.Setup(r => r.ObterTodos()).Returns(aReceber);
            _mapperMock.Setup(m => m.Map<List<AReceberResponseContract>>(aReceber)).Returns(aReceberResponseContract);

            // Act
            var resultado = _aReceberService.ObterTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(aReceberResponseContract.Count, resultado.Count);
            Assert.Equal(aReceberResponseContract[0].Id, resultado[0].Id);
            Assert.Equal(aReceberResponseContract[0].IdPessoa, resultado[0].IdPessoa);
            Assert.Equal(aReceberResponseContract[0].IdNaturezaLancamento, resultado[0].IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract[0].ValorAReceber, resultado[0].ValorAReceber);
            Assert.Equal(aReceberResponseContract[1].Id, resultado[1].Id);
            Assert.Equal(aReceberResponseContract[1].IdPessoa, resultado[1].IdPessoa);
            Assert.Equal(aReceberResponseContract[1].IdNaturezaLancamento, resultado[1].IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract[1].ValorAReceber, resultado[1].ValorAReceber);
        }
    }
}