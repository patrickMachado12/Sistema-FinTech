using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.Pessoa;
using Microsoft.AspNetCore.Mvc;
using FinTech.Test.DataBase;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class PessoaControllerTests : BaseTest, IAsyncLifetime
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
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" },
                new Pessoa { Id = 2, Nome = "Maria", Email = "maria@email.com" }
            };
            var pessoaResponseContract = new List<PessoaResponseContract>
            {
                new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" },
                new PessoaResponseContract { Id = 2, Nome = "Maria", Email = "maria@email.com" }
            };

            _pessoaServiceMock.Setup(s => s.ObterTodos()).ReturnsAsync(pessoas);

            // Act
            var resultado = await _pessoaController.Get();

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
            Assert.Equal(pessoaResponseContract[0].Email, pessoasRetornadas[0].Email);
            Assert.Equal(pessoaResponseContract[1].Id, pessoasRetornadas[1].Id);
            Assert.Equal(pessoaResponseContract[1].Nome, pessoasRetornadas[1].Nome);
            Assert.Equal(pessoaResponseContract[1].Email, pessoasRetornadas[1].Email);
        }

        [Fact(DisplayName = "Deve retornar uma pessoa por ID.")]
        public async Task Get_DeveRetornarPessoa_QuandoPessoaExiste()
        {
            // Arrange
            var id = 1;
            var pessoa = new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _pessoaServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(pessoa);

            // Act
            var resultado = await _pessoaController.Get(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var pessoaRetornada = okResult.Value as PessoaResponseContract;
            Assert.NotNull(pessoaRetornada);
            Assert.Equal(pessoaResponseContract.Id, pessoaRetornada.Id);
            Assert.Equal(pessoaResponseContract.Nome, pessoaRetornada.Nome);
            Assert.Equal(pessoaResponseContract.Email, pessoaRetornada.Email);
        }

        [Fact(DisplayName = "Deve criar uma nova pessoa.")]
        public async Task Post_DeveCriarPessoa_QuandoPessoaEhValida()
        {
            // Arrange
            var pessoaRequestContract = new PessoaRequestContract { Nome = "João", Email = "joao@email.com" };
            var pessoa = new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _pessoaServiceMock.Setup(s => s.Adicionar(pessoaRequestContract)).ReturnsAsync(pessoa);

            // Act
            var resultado = await _pessoaController.Post(pessoaRequestContract);

            // Assert
                        Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var pessoaRetornada = okResult.Value as PessoaResponseContract;
            Assert.NotNull(pessoaRetornada);
            Assert.Equal(pessoaResponseContract.Id, pessoaRetornada.Id);
            Assert.Equal(pessoaResponseContract.Nome, pessoaRetornada.Nome);
            Assert.Equal(pessoaResponseContract.Email, pessoaRetornada.Email);
        }

        [Fact(DisplayName = "Deve atualizar uma pessoa existente.")]
        public async Task Put_DeveAtualizarPessoa_QuandoPessoaExiste()
        {
            // Arrange
            var id = 1;
            var pessoaRequestContract = new PessoaRequestContract { Nome = "João", Email = "joao@email.com" };
            var pessoa = new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" };
            var pessoaResponseContract = new PessoaResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _pessoaServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(pessoa);
            _pessoaServiceMock.Setup(s => s.Atualizar(pessoaRequestContract)).ReturnsAsync(pessoa);

            // Act
            var resultado = await _pessoaController.Put(id, pessoaRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var pessoaRetornada = okResult.Value as PessoaResponseContract;
            Assert.NotNull(pessoaRetornada);
            Assert.Equal(pessoaResponseContract.Id, pessoaRetornada.Id);
            Assert.Equal(pessoaResponseContract.Nome, pessoaRetornada.Nome);
            Assert.Equal(pessoaResponseContract.Email, pessoaRetornada.Email);
        }

        [Fact(DisplayName = "Deve deletar uma pessoa existente.")]
        public async Task Delete_DeveDeletarPessoa_QuandoPessoaExiste()
        {
            // Arrange
            var id = 1;
            var pessoa = new Pessoa { Id = 1, Nome = "João", Email = "joao@email.com" };

            _pessoaServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(pessoa);
            _pessoaServiceMock.Setup(s => s.Deletar(pessoa)).ReturnsAsync(true);

            // Act
            var resultado = await _pessoaController.Delete(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }
    }
}
           