using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Interfaces;
using FinTech.Api.Contract.AReceber;
using Microsoft.AspNetCore.Mvc;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class AReceberControllerTests
    {
        private readonly Mock<ITituloService<AReceberRequestContract, AReceberResponseContract, long>> _aReceberServiceMock;
        private readonly AReceberController _aReceberController;

        public AReceberControllerTests()
        {
            _aReceberServiceMock = new Mock<ITituloService<AReceberRequestContract, AReceberResponseContract, long>>();
            _aReceberController = new AReceberController(_aReceberServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar uma lista de a receber.")]
        public async Task Get_DeveRetornarListaDeAReceber_QuandoAReceberExistem()
        {
            var aReceberResponseContract = new List<AReceberResponseContract>
            {
                new AReceberResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 },
                new AReceberResponseContract { Id = 2, IdNaturezaLancamento = 2, ValorAReceber = 200.00 }
            };

            _aReceberServiceMock.Setup(s => s.ObterTodos(1)).ReturnsAsync(aReceberResponseContract);

            var resultado = await _aReceberController.ObterTodos(1);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.Equal(aReceberResponseContract, okResult.Value);
        }

        [Fact(DisplayName = "Deve retornar um a receber por ID.")]
        public async Task Get_DeveRetornarAReceber_QuandoAReceberExiste()
        {
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };

            _aReceberServiceMock.Setup(s => s.Obter(1, 1)).ReturnsAsync(aReceberResponseContract);

            var resultado = await _aReceberController.Obter(1, 1);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aReceberRetornada = okResult.Value as AReceberResponseContract;
            Assert.NotNull(aReceberRetornada);
            Assert.Equal(aReceberResponseContract.Id, aReceberRetornada.Id);
        }

        [Fact(DisplayName = "Deve criar um novo a receber.")]
        public async Task Post_DeveCriarAReceber_QuandoAReceberEhValida()
        {
            var aReceberRequestContract = new AReceberRequestContract { IdNaturezaLancamento = 1, ValorAReceber = 100.00 };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };

            _aReceberServiceMock.Setup(s => s.Adicionar(aReceberRequestContract, 1)).ReturnsAsync(aReceberResponseContract);

            var resultado = await _aReceberController.Adicionar(aReceberRequestContract);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aReceberRetornada = okResult.Value as AReceberResponseContract;
            Assert.NotNull(aReceberRetornada);
            Assert.Equal(aReceberResponseContract.Id, aReceberRetornada.Id);
        }

        [Fact(DisplayName = "Deve atualizar um a receber existente.")]
        public async Task Put_DeveAtualizarAReceber_QuandoAReceberExiste()
        {
            var id = 1;
            var aReceberRequestContract = new AReceberRequestContract { IdNaturezaLancamento = 1, ValorAReceber = 100.00 };
            var aReceberResponseContract = new AReceberResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAReceber = 100.00 };

             _aReceberServiceMock.Setup(s => s.Atualizar(id, aReceberRequestContract, 1)).ReturnsAsync(aReceberResponseContract);

            var resultado = await _aReceberController.Atualizar(id, aReceberRequestContract);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aReceberRetornada = okResult.Value as AReceberResponseContract;
            Assert.NotNull(aReceberRetornada);
            Assert.Equal(aReceberResponseContract.Id, aReceberRetornada.Id);
        }

        [Fact(DisplayName = "Deve deletar um a receber existente.")]
        public async Task Delete_DeveDeletarAReceber_QuandoAReceberExiste()
        {
            var id = 1;

            _aReceberServiceMock.Setup(s => s.Inativar(id, 1)).Returns(Task.CompletedTask);

            var resultado = await _aReceberController.Deletar(id);

            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }
    }
}
