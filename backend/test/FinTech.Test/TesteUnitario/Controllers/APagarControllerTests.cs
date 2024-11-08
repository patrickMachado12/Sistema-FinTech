using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Interfaces;
using FinTech.Api.Contract.APagar;
using Microsoft.AspNetCore.Mvc;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class APagarControllerTests
    {
        private readonly Mock<ITituloService<APagarRequestContract, APagarResponseContract, long>> _aPagarServiceMock;
        private readonly APagarController _aPagarController;

        public APagarControllerTests()
        {
            _aPagarServiceMock = new Mock<ITituloService<APagarRequestContract, APagarResponseContract, long>>();
            _aPagarController = new APagarController(_aPagarServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar uma lista de a Pagar.")]
        public async Task Get_DeveRetornarListaDeAPagar_QuandoAPagarExistem()
        {
            var aPagarResponseContract = new List<APagarResponseContract>
            {
                new APagarResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 },
                new APagarResponseContract { Id = 2, IdNaturezaLancamento = 2, ValorAPagar = 200.00 }
            };

            _aPagarServiceMock.Setup(s => s.ObterTodos(1)).ReturnsAsync(aPagarResponseContract);

            var resultado = await _aPagarController.ObterTodos(1);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.Equal(aPagarResponseContract, okResult.Value);
        }

        [Fact(DisplayName = "Deve retornar um a Pagar por ID.")]
        public async Task Get_DeveRetornarAPagar_QuandoAPagarExiste()
        {
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };

            _aPagarServiceMock.Setup(s => s.Obter(1, 1)).ReturnsAsync(aPagarResponseContract);

            var resultado = await _aPagarController.Obter(1, 1);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aPagarRetornada = okResult.Value as APagarResponseContract;
            Assert.NotNull(aPagarRetornada);
            Assert.Equal(aPagarResponseContract.Id, aPagarRetornada.Id);
        }

        [Fact(DisplayName = "Deve criar um novo a Pagar.")]
        public async Task Post_DeveCriarAPagar_QuandoAPagarEhValida()
        {
            var aPagarRequestContract = new APagarRequestContract { IdNaturezaLancamento = 1, ValorAPagar = 100.00 };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };

            _aPagarServiceMock.Setup(s => s.Adicionar(aPagarRequestContract, 1)).ReturnsAsync(aPagarResponseContract);

            var resultado = await _aPagarController.Adicionar(aPagarRequestContract);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aPagarRetornada = okResult.Value as APagarResponseContract;
            Assert.NotNull(aPagarRetornada);
            Assert.Equal(aPagarResponseContract.Id, aPagarRetornada.Id);
        }

        [Fact(DisplayName = "Deve atualizar um a Pagar existente.")]
        public async Task Put_DeveAtualizarAPagar_QuandoAPagarExiste()
        {
            var id = 1;
            var aPagarRequestContract = new APagarRequestContract { IdNaturezaLancamento = 1, ValorAPagar = 100.00 };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00 };

            _aPagarServiceMock.Setup(s => s.Atualizar(id, aPagarRequestContract, 1)).ReturnsAsync(aPagarResponseContract);

            var resultado = await _aPagarController.Atualizar(id, aPagarRequestContract);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aPagarRetornada = okResult.Value as APagarResponseContract;
            Assert.NotNull(aPagarRetornada);
            Assert.Equal(aPagarResponseContract.Id, aPagarRetornada.Id);
        }

        [Fact(DisplayName = "Deve deletar um a Pagar existente.")]
        public async Task Delete_DeveDeletarAPagar_QuandoAPagarExiste()
        {
            var id = 1;

            _aPagarServiceMock.Setup(s => s.Inativar(id, 1)).Returns(Task.CompletedTask);

            var resultado = await _aPagarController.Deletar(id);

            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }
    }
}
