using Xunit;
using Moq;
using FinTech.Api.Controllers;
using FinTech.Api.Domain.Services.Interfaces;
using FinTech.Api.Domain.Models;
using FinTech.Api.Contract.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace FinTech.Test.TesteUnitario.Controllers
{
    public class UsuarioControllerTests
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
            var usuariosResponseContracts = new List<UsuarioResponseContract>
            {
                new UsuarioResponseContract { Id = 1, Email = "joao@email.com" },
                new UsuarioResponseContract { Id = 2, Email = "maria@email.com" }
            };

            _usuarioServiceMock.Setup(s => s.ObterTodos(It.IsAny<int>())).ReturnsAsync(usuariosResponseContracts);

            var resultado = await _usuarioController.ObterTodos();

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var usuariosRetornados = Assert.IsAssignableFrom<List<UsuarioResponseContract>>(okResult.Value);
            Assert.Equal(usuariosResponseContracts.Count, usuariosRetornados.Count);
            Assert.Equal(usuariosResponseContracts[0].Id, usuariosRetornados[0].Id);
            Assert.Equal(usuariosResponseContracts[0].Email, usuariosRetornados[0].Email);
            Assert.Equal(usuariosResponseContracts[1].Id, usuariosRetornados[1].Id);
            Assert.Equal(usuariosResponseContracts[1].Email, usuariosRetornados[1].Email);
        }

        [Fact(DisplayName = "Deve retornar um usuário por ID.")]
        public async Task Get_DeveRetornarUsuario_QuandoUsuarioExiste()
        {
            var usuarioResponseContract = new UsuarioResponseContract { Id = 1, Email = "joao@email.com" };

            _usuarioServiceMock.Setup(s => s.Obter("1")).ReturnsAsync(usuarioResponseContract);

            var resultado = await _usuarioController.Obter(1);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var usuarioRetornado = Assert.IsAssignableFrom<UsuarioResponseContract>(okResult.Value);
            Assert.Equal(usuarioResponseContract.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioResponseContract.Email, usuarioRetornado.Email);
        }

        [Fact(DisplayName = "Deve criar um novo usuário.")]
        public async Task Post_DeveCriarUsuario_QuandoUsuarioEhValido()
        {
            var usuarioRequestContract = new UsuarioRequestContract { Email = "joao@email.com" };
            var usuarioResponseContract = new UsuarioResponseContract { Id = 1, Email = "joao@email.com" };

            _usuarioServiceMock.Setup(s => s.Adicionar(usuarioRequestContract, It.IsAny<int>())).ReturnsAsync(usuarioResponseContract);

            var resultado = await _usuarioController.Adicionar(usuarioRequestContract);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var usuarioRetornado = Assert.IsAssignableFrom<UsuarioResponseContract>(okResult.Value);
            Assert.Equal(usuarioResponseContract.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioResponseContract.Email, usuarioRetornado.Email);
        }

        [Fact(DisplayName = "Deve atualizar um usuário existente.")]
        public async Task Put_DeveAtualizarUsuario_QuandoUsuarioExiste()
        {
            var usuarioRequestContract = new UsuarioRequestContract { Email = "joao@email.com" };
            var usuarioResponseContract = new UsuarioResponseContract { Id = 1, Email = "joao@email.com" };

            _usuarioServiceMock.Setup(s => s.Obter("1")).ReturnsAsync(usuarioResponseContract);
            _usuarioServiceMock.Setup(s => s.Atualizar(1, usuarioRequestContract, It.IsAny<int>())).ReturnsAsync(usuarioResponseContract);

            var resultado = await _usuarioController.Atualizar(1, usuarioRequestContract);

            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var usuarioRetornado = Assert.IsAssignableFrom<UsuarioResponseContract>(okResult.Value);
            Assert.Equal(usuarioResponseContract.Id, usuarioRetornado.Id);
            Assert.Equal(usuarioResponseContract.Email, usuarioRetornado.Email);
        }

        [Fact(DisplayName = "Deve deletar um usuário existente.")]
        public async Task Delete_DeveDeletarUsuario_QuandoUsuarioExiste()
        {
            var usuario = new Usuario { Email = "joao@email.com" };

            _usuarioServiceMock.Setup(s => s.Obter("1")).ReturnsAsync(new UsuarioResponseContract { Id = 1, Email = "joao@email.com" });
            _usuarioServiceMock.Setup(s => s.Inativar(1, It.IsAny<int>())).Returns(Task.CompletedTask);

            var resultado = await _usuarioController.Deletar(1);

            Assert.IsType<OkResult>(resultado);
        }
    }
}
