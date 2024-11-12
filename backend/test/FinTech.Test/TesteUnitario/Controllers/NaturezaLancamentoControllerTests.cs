using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Interfaces;
using FinTech.Api.Contract.NaturezaLancamento;
using Microsoft.AspNetCore.Mvc;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class NaturezaLancamentoControllerTests
    {
        private readonly Mock<IService<NaturezaLancamentoRequestContract, NaturezaLancamentoResponseContract, long>> _naturezaLancamentoServiceMock;
        private readonly NaturezaLancamentoController _naturezaLancamentoController;

        public NaturezaLancamentoControllerTests()
        {
            _naturezaLancamentoServiceMock = new Mock<IService<NaturezaLancamentoRequestContract, NaturezaLancamentoResponseContract, long>>();
            _naturezaLancamentoController = new NaturezaLancamentoController(_naturezaLancamentoServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar uma lista de naturezas de lançamento.")]
        public async Task Get_DeveRetornarListaDeNaturezasDeLancamento_QuandoNaturezasDeLancamentoExistem()
        {
            var naturezasDeLancamentoResponseContract = new List<NaturezaLancamentoResponseContract>
            {
                new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Natureza de lançamento 1" },
                new NaturezaLancamentoResponseContract { Id = 2, Descricao = "Natureza de lançamento 2" }
            };

            _naturezaLancamentoServiceMock.Setup(s => s.ObterTodos(1)).ReturnsAsync(naturezasDeLancamentoResponseContract);

            var resultado = await _naturezaLancamentoController.ObterTodos();

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado;
            Assert.Equal(naturezasDeLancamentoResponseContract, okResult.Value);
        }

        [Fact(DisplayName = "Deve retornar uma natureza de lançamento por ID.")]
        public async Task Get_DeveRetornarNaturezaDeLancamento_QuandoNaturezaDeLancamentoExiste()
        {
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Natureza de lançamento 1" };

            _naturezaLancamentoServiceMock.Setup(s => s.Obter(1, 1)).ReturnsAsync(naturezaLancamentoResponseContract);

            var resultado = await _naturezaLancamentoController.Obter(1);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado;
            var naturezaDeLancamentoRetornada = okResult.Value as NaturezaLancamentoResponseContract;
            Assert.Equal(naturezaLancamentoResponseContract.Id, naturezaDeLancamentoRetornada.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, naturezaDeLancamentoRetornada.Descricao);
        }

        [Fact(DisplayName = "Deve criar uma nova natureza de lançamento.")]
        public async Task Post_DeveCriarNaturezaDeLancamento_QuandoNaturezaDeLancamentoEhValida()
        {
            var naturezaLancamentoRequestContract = new NaturezaLancamentoRequestContract { Descricao = "Natureza de lançamento 1" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Natureza de lançamento 1" };

            _naturezaLancamentoServiceMock.Setup(s => s.Adicionar(naturezaLancamentoRequestContract, 1)).ReturnsAsync(naturezaLancamentoResponseContract);

            var resultado = await _naturezaLancamentoController.Adicionar(naturezaLancamentoRequestContract);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);

            var okResult = resultado;
            var naturezaDeLancamentoRetornada = okResult.Value as NaturezaLancamentoResponseContract;

            Assert.Equal(naturezaLancamentoResponseContract.Id, naturezaDeLancamentoRetornada.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, naturezaDeLancamentoRetornada.Descricao);
        }

        [Fact(DisplayName = "Deve atualizar uma natureza de lançamento existente.")]
        public async Task Put_DeveAtualizarNaturezaDeLancamento_QuandoNaturezaDeLancamentoExiste()
        {
            var id = 1;
            var naturezaLancamentoRequestContract = new NaturezaLancamentoRequestContract { Descricao = "Natureza de lançamento atualizada" };
            var naturezaLancamentoResponseContract = new NaturezaLancamentoResponseContract { Id = 1, Descricao = "Natureza de lançamento atualizada" };

            _naturezaLancamentoServiceMock.Setup(s => s.Atualizar(id, naturezaLancamentoRequestContract, 1)).ReturnsAsync(naturezaLancamentoResponseContract);

            var resultado = await _naturezaLancamentoController.Atualizar(id, naturezaLancamentoRequestContract);

            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);

            var okResult = resultado;
            var naturezaDeLancamentoRetornada = okResult.Value as NaturezaLancamentoResponseContract;
            
            Assert.Equal(naturezaLancamentoResponseContract.Id, naturezaDeLancamentoRetornada.Id);
            Assert.Equal(naturezaLancamentoResponseContract.Descricao, naturezaDeLancamentoRetornada.Descricao);
        }

        [Fact(DisplayName = "Deve deletar uma natureza de lançamento existente.")]
        public async Task Delete_DeveDeletarNaturezaDeLancamento_QuandoNaturezaDeLancamentoExiste()
        {
            var id = 1;
            _naturezaLancamentoServiceMock.Setup(s => s.Inativar(id, 1)).Returns(Task.CompletedTask);

            var resultado = await _naturezaLancamentoController.Deletar(id);

            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }
    }
}
