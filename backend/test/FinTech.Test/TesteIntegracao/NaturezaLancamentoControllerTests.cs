using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.NaturezaLancamento;
using Microsoft.AspNetCore.Mvc;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class NaturezaLancamentoControllerTests
    {
        private readonly Mock<INaturezaLancamentoService> _naturezaLancamentoServiceMock;
        private readonly NaturezaLancamentoController _naturezaLancamentoController;

        public NaturezaLancamentoControllerTests()
        {
            _naturezaLancamentoServiceMock = new Mock<INaturezaLancamentoService>();
            _naturezaLancamentoController = new NaturezaLancamentoController(_naturezaLancamentoServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar uma lista de naturezas de lançamento.")]
        public async Task Get_DeveRetornarListaDeNaturezasDeLancamento_QuandoNaturezasDeLancamentoExistem()
        {
            // Arrange
            var naturezasDeLancamento = new List<NaturezaLancamento>
            {
                new NaturezaLancamento { Id = 1, Descricao = "Natureza de lançamento 1" },
                new NaturezaLancamento { Id = 2, Descricao = "Natureza de lançamento 2" }
            };
            var naturezaLancamentoResponseContract = new List<NaturezaLancamentoResponseContract>
            {
                new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Natureza de lançamento 1" },
                new NaturezaLancamentoResponseContract { Id = 2, Descricao = "Natureza de lançamento 2" }
            };

            _naturezaLancamentoServiceMock.Setup(s => s.ObterTodos()).ReturnsAsync(naturezasDeLancamento);

            // Act
            var resultado = await _naturezaLancamentoController.Get();

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var naturezasDeLancamentoRetornadas = okResult.Value as List<NaturezaLancamentoResponseContract>;
            Assert.NotNull(naturezasDeLancamentoRetornadas);
            Assert.Equal(naturezaLancamentoResponseContract.Count, naturezasDeLancamentoRetornadas.Count);
            Assert.Equal(naturezaLancamentoResponseContract[0].Id, naturezasDeLancamentoRetornadas[0].Id);
            Assert.Equal(naturezaLancamentoResponseContract[0].Descricao, naturezasDeLancamentoRetornadas[0].Descricao);
            Assert.Equal(naturezaLancamentoResponseContract[1].Id, naturezasDeLancamentoRetornadas[1].Id);
            Assert.Equal(naturezaLancamentoResponseContract[1].Descricao, naturezasDeLancamentoRetornadas[1].Descricao);
        }

        [Fact(DisplayName = "Deve retornar uma natureza de lançamento por ID.")]
        public async Task Get_DeveRetornarNaturezaDeLancamento_QuandoNaturezaDeLancamentoExiste()
        {
            // Arrange
            var id = 1;
            var naturezaDeLancamento = new NaturezaLancamento { Id = 1, Descricao = "Natureza de lançamento 1" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Natureza de lançamento 1" };

            _naturezaLancamentoServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(naturezaDeLancamento);

            // Act
            var resultado = await _naturezaLancamentoController.Get(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var naturezaDeLancamentoRetornada = okResult.Value as NaturezaLancamentoResponseContract;
            Assert.NotNull(naturezaDeLancamentoRetornada);
            Assert.Equal(naturezaLancamentoResponseContract.Id, naturezaDeLancamentoRetornada.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, naturezaDeLancamentoRetornada.Descricao);
        }

        [Fact(DisplayName = "Deve criar uma nova natureza de lançamento.")]
        public async Task Post_DeveCriarNaturezaDeLancamento_QuandoNaturezaDeLancamentoEhValida()
        {
            // Arrange
            var naturezaLancamentoRequestContract = new NaturezaLancamentoRequestContract { Descricao = "Natureza de lançamento 1" };
            var naturezaDeLancamento = new NaturezaLancamento { Id = 1, Descricao = "Natureza de lançamento 1" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Natureza de lançamento 1" };

            _naturezaLancamentoServiceMock.Setup(s => s.Adicionar(naturezaLancamentoRequestContract)).ReturnsAsync(naturezaDeLancamento);

            // Act
            var resultado = await _naturezaLancamentoController.Post(naturezaLancamentoRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var naturezaDeLancamentoRetornada = okResult.Value as NaturezaLancamentoResponseContract;
            Assert.NotNull(naturezaDeLancamentoRetornada);
            Assert.Equal(naturezaLancamentoResponseContract.Id, naturezaDeLancamentoRetornada.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, naturezaDeLancamentoRetornada.Descricao);
        }

        [Fact(DisplayName = "Deve atualizar uma natureza de lançamento existente.")]
        public async Task Put_DeveAtualizarNaturezaDeLancamento_QuandoNaturezaDeLancamentoExiste()
        {
            // Arrange
            var id = 1;
            var naturezaLancamentoRequestContract = new NaturezaLancamentoRequestContract { Descricao = "Natureza de lançamento 1" };
            var naturezaDeLancamento = new NaturezaLancamento { Id = 1, Descricao = "Natureza de lançamento 1" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Natureza de lançamento 1" };

            _naturezaLancamentoServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(naturezaDeLancamento);
            _naturezaLancamentoServiceMock.Setup(s => s.Atualizar(naturezaLancamentoRequestContract)).ReturnsAsync(naturezaDeLancamento);

            // Act
            var resultado = await _naturezaLancamentoController.Put(id, naturezaLancamentoRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var naturezaDeLancamentoRetornada = okResult.Value as NaturezaLancamentoResponseContract;
            Assert.NotNull(naturezaDeLancamentoRetornada);
            Assert.Equal(naturezaLancamentoResponseContract.Id, naturezaDeLancamentoRetornada.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, naturezaDeLancamentoRetornada.Descricao);
        }

        [Fact(DisplayName = "Deve deletar uma natureza de lançamento existente.")]
        public async Task Delete_DeveDeletarNaturezaDeLancamento_QuandoNaturezaDeLancamentoExiste()
        {
            // Arrange
            var id = 1;
            var naturezaDeLancamento = new NaturezaLancamento { Id = 1, Descricao = "Natureza de lançamento 1" };

            _naturezaLancamentoServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(naturezaDeLancamento);
            _naturezaLancamentoServiceMock.Setup(s => s.Deletar(naturezaDeLancamento)).ReturnsAsync(true);

            // Act
            var resultado = await _naturezaLancamentoController.Delete(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }
    }
}