using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.Usuario;
using Microsoft.AspNetCore.Mvc;
using FinTech.Test.DataBase;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class UsuarioControllerTests : BaseTest, IAsyncLifetime
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly UsuarioController _usuarioController;

        public UsuarioControllerTests()
        {
            _usuarioServiceMock = new Mock<IUsuarioService>();
            _usuarioController = new UsuarioController(_usuarioServiceMock.Object);
        }

        [Fact(DisplayName = "Deve retornar uma lista de usuários.")]
        public async Task Get_DeveRetornarListaDeUsuarios_QuandoUsuariosExistem()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "João", Email = "joao@email.com" },
                new Usuario { Id = 2, Nome = "Maria", Email = "maria@email.com" }
            };
            var usuarioResponseContract = new List<UsuarioResponseContract>
            {
                new UsuarioResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" },
                new UsuarioResponseContract { Id = 2, Nome = "Maria", Email = "maria@email.com" }
            };

            _usuarioServiceMock.Setup(s => s.ObterTodos()).ReturnsAsync(usuarios);

            // Act
            var resultado = await _usuarioController.Get();

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var usuariosRetornados = okResult.Value as List<UsuarioResponseContract>;
            Assert.NotNull(usuariosRetornados);
            Assert.Equal(usuarioResponseContract.Count, usuariosRetornados.Count);
            Assert.Equal(usuarioResponseContract[0].Id, usuariosRetornados[0].Id);
            Assert.Equal(usuarioResponseContract[0].Nome, usuariosRetornados[0].Nome);
            Assert.Equal(usuarioResponseContract[0].Email, usuariosRetornados[0].Email);
            Assert.Equal(usuarioResponseContract[1].Id, usuariosRetornados[1].Id);
            Assert.Equal(usuarioResponseContract[1].Nome, usuariosRetornados[1].Nome);
            Assert.Equal(usuarioResponseContract[1].Email, usuariosRetornados[1].Email);
        }

        [Fact(DisplayName = "Deve retornar um usuário por ID.")]
        public async Task Get_DeveRetornarUsuario_QuandoUsuarioExiste()
        {
            // Arrange
            var id = 1;
            var usuario = new Usuario { Id = 1, Nome = "João", Email = "joao@email.com" };
            var usuarioResponseContract = new UsuarioResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _usuarioServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(usuario);

            // Act
            var resultado = await _usuarioController.Get(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var usuarioRetornado = okResult.Value as UsuarioResponseContract;
            Assert.NotNull(usuarioRetornado);
            Assert.Equal(usuarioResponseContract.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioResponseContract.Nome, usuarioRetornado.Nome);
            Assert.Equal(usuarioResponseContract.Email, usuarioRetornado.Email);
        }

        [Fact(DisplayName = "Deve criar um novo usuário.")]
        public async Task Post_DeveCriarUsuario_QuandoUsuarioEhValido()
        {
            // Arrange
            var usuarioRequestContract = new UsuarioRequestContract { Nome = "João", Email = "joao@email.com" };
            var usuario = new Usuario { Id = 1, Nome = "João", Email = "joao@email.com" };
            var usuarioResponseContract = new UsuarioResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _usuarioServiceMock.Setup(s => s.Adicionar(usuarioRequestContract)).ReturnsAsync(usuario);

            // Act
            var resultado = await _usuarioController.Post(usuarioRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var usuarioRetornado = okResult.Value as UsuarioResponseContract
                        Assert.NotNull(usuarioRetornado);
            Assert.Equal(usuarioResponseContract.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioResponseContract.Nome, usuarioRetornado.Nome);
            Assert.Equal(usuarioResponseContract.Email, usuarioRetornado.Email);
        }

        [Fact(DisplayName = "Deve atualizar um usuário existente.")]
        public async Task Put_DeveAtualizarUsuario_QuandoUsuarioExiste()
        {
            // Arrange
            var id = 1;
            var usuarioRequestContract = new UsuarioRequestContract { Nome = "João", Email = "joao@email.com" };
            var usuario = new Usuario { Id = 1, Nome = "João", Email = "joao@email.com" };
            var usuarioResponseContract = new UsuarioResponseContract { Id = 1, Nome = "João", Email = "joao@email.com" };

            _usuarioServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(usuario);
            _usuarioServiceMock.Setup(s => s.Atualizar(usuarioRequestContract)).ReturnsAsync(usuario);

            // Act
            var resultado = await _usuarioController.Put(id, usuarioRequestContract);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkObjectResult>(resultado);
            var okResult = resultado as OkObjectResult;
            Assert.NotNull(okResult);
            var usuarioRetornado = okResult.Value as UsuarioResponseContract;
            Assert.NotNull(usuarioRetornado);
            Assert.Equal(usuarioResponseContract.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioResponseContract.Nome, usuarioRetornado.Nome);
            Assert.Equal(usuarioResponseContract.Email, usuarioRetornado.Email);
        }

        [Fact(DisplayName = "Deve deletar um usuário existente.")]
        public async Task Delete_DeveDeletarUsuario_QuandoUsuarioExiste()
        {
            // Arrange
            var id = 1;
            var usuario = new Usuario { Id = 1, Nome = "João", Email = "joao@email.com" };

            _usuarioServiceMock.Setup(s => s.ObterPorId(id)).ReturnsAsync(usuario);
            _usuarioServiceMock.Setup(s => s.Deletar(usuario)).ReturnsAsync(true);

            // Act
            var resultado = await _usuarioController.Delete(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<OkResult>(resultado);
        }
    }
}