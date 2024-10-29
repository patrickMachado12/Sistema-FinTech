using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.AReceber;
using Microsoft.AspNetCore.Mvc;
using FinTech.Test.DataBase;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class AReceberControllerTests : BaseTest, IAsyncLifetime
    {
        private readonly Mock<IAReceberService> _aReceberServiceMock;
        private readonly AReceberController _aReceberController;

        public AReceberControllerTests()
        {
            _aReceberServiceMock = new Mock<IAReceberService>();
            _aReceberController = new AReceberController(_aReceberServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar uma lista de a receber.")]
        public async Task Get_DeveRetornarListaDeAReceber_QuandoAReceberExistem()
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

            _aReceberServiceMock.Setup(s => s.ObterTodos()).ReturnsAsync(aReceber);

            // Act
            var resultado = await _aReceberController.Get();

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aReceberRetornadas = okResult.Value as List<AReceberResponseContract>;
            Assert.NotNull(aReceberRetornadas);
            Assert.Equal(aReceberResponseContract.Count, aReceberRetornadas.Count);
            Assert.Equal(aReceberResponseContract[0].Id, aReceberRetornadas[0].Id);
            Assert.Equal(aReceberResponseContract[0].IdPessoa, aReceberRetornadas[0].IdPessoa);
            Assert.Equal(aReceberResponseContract[0].IdNaturezaLancamento, aReceberRetornadas[0].IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract[0].ValorAReceber, aReceberRetornadas[0].ValorAReceber);
            Assert.Equal(aReceberResponseContract[1].Id, aReceberRetornadas[1].Id);
            Assert.Equal(aReceberResponseContract[1].IdPessoa, aReceberRetornadas[1].IdPessoa);
            Assert.Equal(aReceberResponseContract[1].IdNaturezaLancamento, aReceberRetornadas[1].IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract[1].ValorAReceber, aReceberRetornadas[1].ValorAReceber);
        }

        [Fact(DisplayName = "Deve retornar um a receber por ID.")]
        public async Task Get_DeveRetornarAReceber_QuandoAReceberExiste()
        {
            // Arrange
            var id = 1;
            var aReceber = new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };

            _aReceberServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(aReceber);

            // Act
            var resultado = await _aReceberController.Get(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aReceberRetornada = okResult.Value as AReceberResponseContract;
            Assert.NotNull(aReceberRetornada);
            Assert.Equal(aReceberResponseContract.Id, aReceberRetornada.Id);
            Assert.Equal(aReceberResponseContract.IdPessoa, aReceberRetornada.IdPessoa);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, aReceberRetornada.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, aReceberRetornada.ValorAReceber);
        }

        [Fact(DisplayName = "Deve criar um novo a receber.")]
        public async Task Post_DeveCriarAReceber_QuandoAReceberEhValida()
        {
            // Arrange
            var aReceberRequestContract = new AReceberRequestContract { IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceber = new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };

            _aReceberServiceMock.Setup(s => s.Adicionar(aReceberRequestContract)).ReturnsAsync(aReceber);

            // Act
            var resultado = await _aReceberController.Post(aReceberRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aReceberRetornada = okResult.Value as AReceberResponseContract;
            Assert.NotNull(aReceberRetornada);
            Assert.Equal(aReceberResponseContract.Id, aReceberRetornada.Id);
            Assert.Equal(aReceberResponseContract.IdPessoa, aReceberRetornada.IdPessoa);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, aReceberRetornada.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, aReceberRetornada.ValorAReceber);
        }

        [Fact(DisplayName = "Deve atualizar um a receber existente.")]
        public async Task Put_DeveAtualizarAReceber_QuandoAReceberExiste()
        {
            // Arrange
            var id = 1;
            var aReceberRequestContract = new AReceberRequestContract { IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceber = new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };

            _aReceberServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(aReceber);
            _aReceberServiceMock.Setup(s => s.Atualizar(aReceberRequestContract)).ReturnsAsync(aReceber);

            // Act
            var resultado = await _aReceberController.Put(id, aReceberRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aReceberRetornada = okResult.Value as AReceberResponseContract;
            Assert.NotNull(aReceberRetornada);
            Assert.Equal(aReceberResponseContract.Id, aReceberRetornada.Id);
            Assert.Equal(aReceberResponseContract.IdPessoa, aReceberRetornada.IdPessoa);
            Assert.Equal(aReceberResponseContract.IdNaturezaLancamento, aReceberRetornada.IdNaturezaLancamento);
            Assert.Equal(aReceberResponseContract.ValorAReceber, aReceberRetornada.ValorAReceber);
        }

        [Fact(DisplayName = "Deve deletar um a receber existente.")]
        public async Task Delete_DeveDeletarAReceber_QuandoAReceberExiste()
        {
            // Arrange
            var id = 1;
            var aReceber = new AReceber { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00m };

            _aReceberServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(aReceber);
            _aReceberServiceMock.Setup(s => s.Deletar(aReceber)).ReturnsAsync(true);

            // Act
            var resultado = await _aReceberController.Delete(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }

        [Fact(DisplayName = "Deve retornar NotFound quando a receber não existe.")]
        public async Task Get_DeveRetornarNotFound_QuandoAReceberNaoExiste()
        {
            // Arrange
            var id = 1;

            _aReceberServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync((AReceber)null);

            // Act
            var resultado = await _aReceberController.Get(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<NotFoundResult>(resultado);
        }

        [Fact(DisplayName = "Deve retornar BadRequest quando a receber é inválida.")]
        public async Task Post_DeveRetornarBadRequest_QuandoAReceberEhInvalida()
        {
            // Arrange
            var aReceberRequestContract = new AReceberRequestContract { IdPessoa = 0, IdNaturezaLancamento = 0, ValorAReceber = 0.00m };

            // Act
            var resultado = await _aReceberController.Post(aReceberRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<BadRequestResult>(resultado);
        }
    }
}