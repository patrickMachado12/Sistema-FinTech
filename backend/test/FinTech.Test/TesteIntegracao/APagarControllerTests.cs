using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Controllers;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class APagarControllerTests
    {
        private readonly Mock<AReceberService> _aReceberServiceMock;
        private readonly Mock<APagarService> _apagarServiceMock;
        private readonly APagarController _apagarController;

        public APagarControllerTests()
        {
            _aReceberServiceMock = new Mock<IAReceberService>();
            _apagarServiceMock = new Mock<IApagarService>();
            _apagarController = new APagarController(_aReceberServiceMock.Object, _apagarServiceMock.Object);
        }

        [Fact(DisplayName = "Deve criar um novo a pagar.")]
        public async Task Post_DeveCriarAPagar_QuandoAPagarEhValida()
        {
            // Arrange
            var aPagarRequestContract = new APagarRequestContract { IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };

            _apagarServiceMock.Setup(s => s.Adicionar(aPagarRequestContract)).ReturnsAsync(aPagar);

            // Act
            var resultado = await _apagarController.Post(aPagarRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aPagarRetornada = okResult.Value as APagarResponseContract;
            Assert.NotNull(aPagarRetornada);
            Assert.Equal(aPagarResponseContract.Id, aPagarRetornada.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, aPagarRetornada.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, aPagarRetornada.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, aPagarRetornada.ValorAPagar);
        }

        [Fact(DisplayName = "Deve atualizar um a pagar existente.")]
        public async Task Put_DeveAtualizarAPagar_QuandoAPagarExiste()
        {
            // Arrange
            var id = 1;
            var aPagarRequestContract = new APagarRequestContract { IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };

            _apagarServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(aPagar);
            _apagarServiceMock.Setup(s => s.Atualizar(aPagarRequestContract)).ReturnsAsync(aPagar);

            // Act
            var resultado = await _apagarController.Put(id, aPagarRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aPagarRetornada = okResult.Value as APagarResponseContract;
            Assert.NotNull(aPagarRetornada);
            Assert.Equal(aPagarResponseContract.Id, aPagarRetornada.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, aPagarRetornada.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, aPagarRetornada.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, aPagarRetornada.ValorAPagar);
        }

        [Fact(DisplayName = "Deve deletar um a pagar existente.")]
        public async Task Delete_DeveDeletarAPagar_QuandoAPagarExiste()
        {
            // Arrange
            var id = 1;
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };

            _apagarServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(aPagar);
            _apagarServiceMock.Setup(s => s.Deletar(aPagar)).ReturnsAsync(true);

            // Act
            var resultado = await _apagarController.Delete(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }

        [Fact(DisplayName = "Deve retornar NotFound quando a pagar não existe.")]
        public async Task Get_DeveRetornarNotFound_QuandoAPagarNaoExiste()
        {
            // Arrange
            var id = 1;

            _apagarServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync((APagar)null);

            // Act
            var resultado = await _apagarController.Get(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<NotFoundResult>(resultado);
        }

        [Fact(DisplayName = "Deve retornar BadRequest quando a pagar é inválida.")]
        public async Task Post_DeveRetornarBadRequest_QuandoAPagarEhInvalida()
        {
            // Arrange
            var aPagarRequestContract = new APagarRequestContract { IdPessoa = 0, IdNaturezaLancamento = 0, ValorAPagar = 0.00m };

            // Act
            var resultado = await _apagarController.Post(aPagarRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<BadRequestResult>(resultado);
        }

        [Fact(DisplayName = "Deve retornar um a pagar por ID.")]
        public async Task Get_DeveRetornarAPagar_QuandoAPagarExiste()
        {
            // Arrange
            var id = 1;
            var aPagar = new APagar { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };
            var aPagarResponseContract = new APagarResponseContract { Id = 1, IdPessoa = 1, IdNaturezaLancamento = 1, ValorAPagar = 100.00m };

            _apagarServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(aPagar);

            // Act
            var resultado = await _apagarController.Get(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var aPagarRetornada = okResult.Value as APagarResponseContract;
            Assert.NotNull(aPagarRetornada);
            Assert.Equal(aPagarResponseContract.Id, aPagarRetornada.Id);
            Assert.Equal(aPagarResponseContract.IdPessoa, aPagarRetornada.IdPessoa);
            Assert.Equal(aPagarResponseContract.IdNaturezaLancamento, aPagarRetornada.IdNaturezaLancamento);
            Assert.Equal(aPagarResponseContract.ValorAPagar, aPagarRetornada.ValorAPagar);
        }
    }
}
       