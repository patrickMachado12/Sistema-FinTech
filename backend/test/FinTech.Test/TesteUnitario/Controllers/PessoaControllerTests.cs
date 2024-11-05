using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.Pessoa;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class PessoaControllerTests
    {
        private readonly Mock<IPessoaService> _pessoaServiceMock;
        private readonly PessoaController _pessoaController;

        public PessoaControllerTests()
        {
            _pessoaServiceMock = new Mock<IPessoaService>();
            _pessoaController = new PessoaController(_pessoaServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar uma lista de pessoas.")]
        public async Task Get_DeveRetornarListaDePessoas_QuandoPessoasExistem()
        {
            // Arrange
            var pessoaResponseContract = new List<PessoaResponseContract>
            {
                new PessoaResponseContract { Id = 1, Nome = "João" },
                new PessoaResponseContract { Id = 2, Nome = "Maria" }
            };

            _pessoaServiceMock.Setup(s => s.ObterTodos()).ReturnsAsync(pessoaResponseContract);

            // Act
            var resultado = await _pessoaController.ObterTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;

            Assert.NotNull(okResult);

            var pessoasRetornadas = okResult.Value as List<PessoaResponseContract>;

            Assert.NotNull(pessoasRetornadas);
            Assert.Equal(pessoaResponseContract.Count, pessoasRetornadas.Count);
            Assert.Equal(pessoaResponseContract[0].Id, pessoasRetornadas[0].Id);
            Assert.Equal(pessoaResponseContract[0].Nome, pessoasRetornadas[0].Nome);
            Assert.Equal(pessoaResponseContract[1].Id, pessoasRetornadas[1].Id);
            Assert.Equal(pessoaResponseContract[1].Nome, pessoasRetornadas[1].Nome);
        }

        [Fact(DisplayName = "Deve retornar uma pessoa por ID.")]
        public async Task Get_DeveRetornarPessoa_QuandoPessoaExiste()
        {
            // Arrange
            var id = 1;
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João" };

            _pessoaServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(pessoaResponseContract);

            // Act
            var resultado = await _pessoaController.ObterPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;

            Assert.NotNull(okResult);

            var pessoaRetornada = okResult.Value as PessoaResponseContract;
            Assert.NotNull(pessoaRetornada);
            Assert.Equal(pessoaResponseContract.Id, pessoaRetornada.Id);
            Assert.Equal(pessoaResponseContract.Nome, pessoaRetornada.Nome);
        }

        [Fact(DisplayName = "Deve criar uma nova pessoa.")]
        public async Task Post_DeveCriarPessoa_QuandoPessoaEhValida()
        {
            // Arrange
            var pessoaRequestContract = new PessoaRequestContract { Nome = "João" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João" };

            _pessoaServiceMock.Setup(s => s.Adicionar(pessoaRequestContract)).ReturnsAsync(pessoaResponseContract);

            // Act
            var resultado = await _pessoaController.Adicionar(pessoaRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var pessoaRetornada = okResult.Value as PessoaResponseContract;
            Assert.NotNull(pessoaRetornada);
            Assert.Equal(pessoaResponseContract.Id, pessoaRetornada.Id);
            Assert.Equal(pessoaResponseContract.Nome, pessoaRetornada.Nome);
        }

        [Fact(DisplayName = "Deve atualizar uma pessoa existente.")]
        public async Task Put_DeveAtualizarPessoa_QuandoPessoaExiste()
        {
            // Arrange
            var id = 1;
            var pessoaRequestContract = new PessoaRequestContract { Nome = "João" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João" };

            _pessoaServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(pessoaResponseContract);
            _pessoaServiceMock.Setup(s => s.Atualizar(id, pessoaRequestContract)).ReturnsAsync(pessoaResponseContract);

            // Act
            var resultado = await _pessoaController.Atualizar(id, pessoaRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var pessoaRetornada = okResult.Value as PessoaResponseContract;
            Assert.NotNull(pessoaRetornada);
            Assert.Equal(pessoaResponseContract.Id, pessoaRetornada.Id);
            Assert.Equal(pessoaResponseContract.Nome, pessoaRetornada.Nome);
        }

        [Fact(DisplayName = "Deve deletar uma pessoa existente.")]
        public async Task Delete_DeveDeletarPessoa_QuandoPessoaExiste()
        {
            // Arrange
            var id = 1;

            _pessoaServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(new PessoaResponseContract { Id = id, Nome = "João" });

            // Act
            var resultado = await _pessoaController.Deletar(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }
        
        // Teste adicional para quando a pessoa não é encontrada
        [Fact(DisplayName = "Deve retornar NotFound quando a pessoa não existe.")]
        public async Task Delete_DeveRetornarNotFound_QuandoPessoaNaoExiste()
        {
            // Arrange
            var id = 1;
            _pessoaServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync((PessoaResponseContract)null);

            // Act
            var resultado = await _pessoaController.Deletar(id);

            // Assert
            Assert.IsType<NotFoundResult>(resultado);
        }
    }
}
