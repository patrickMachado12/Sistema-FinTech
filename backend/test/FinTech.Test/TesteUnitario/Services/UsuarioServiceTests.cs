using Xunit;
using Moq;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Models;
using AutoMapper;
using FinTech.Api.Contract.Usuario;

namespace FinTech.Test.TesteUnitario.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly UsuarioService _usuarioService;

        private TokenService CreateTokenServiceMock()
        {
            return new TokenService(null);
        }

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            var mapperMock = new Mock<IMapper>();

            var tokenServiceMock = CreateTokenServiceMock();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, mapperMock.Object, tokenServiceMock);
        }

        private UsuarioRequestContract ToRequestContract(Usuario usuario)
        {
            return new UsuarioRequestContract
            {
                Email = usuario.Email,
                Senha = usuario.Senha
            };
        }

        [Fact(DisplayName = "Deve realizar o cadastro de um novo usuário.")]
        public async Task Cadastrar_DeveRetornarTrue_QuandoUsuarioEhValido()
        {
            var usuario = new Usuario { Email = "joao@email.com", Senha = "123456" };
            var usuarioRequestContract = ToRequestContract(usuario);
            _usuarioRepositoryMock.Setup(r => r.Adicionar(usuario)).ReturnsAsync(new Usuario { Id = 1, Email = "joao@email.com", Senha = "123456" });

            var resultado = await _usuarioService.Adicionar(usuarioRequestContract, 1);

            Assert.NotNull(resultado);
        }


        [Fact(DisplayName = "Não deve realizar o cadastro de um novo usuário.")]
        public async Task Cadastrar_DeveRetornarFalse_QuandoUsuarioEhInvalido()
        {
            var usuario = new Usuario { Email = string.Empty, Senha = string.Empty };
            var usuarioRequestContract = ToRequestContract(usuario);
            _usuarioRepositoryMock.Setup(r => r.Adicionar(usuario)).ReturnsAsync((Usuario)null);

            var resultado = await _usuarioService.Adicionar(usuarioRequestContract, 1);

            Assert.Null(resultado);
        }

        [Fact(DisplayName = "Deve obter um usuário por ID.")]
        public void ObterPorId_DeveRetornarUsuario_QuandoIdEhValido()
        {
            var id = 1;
            var usuario = new Usuario { Id = id, Email = "joao@email.com", Senha = "123456",};
            _usuarioRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(usuario);

            var resultado = _usuarioService.Obter(id, 1);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.Id);
        }

        [Fact(DisplayName = "Não deve obter um usuário por ID.")]
        public void ObterPorId_DeveRetornarNull_QuandoIdEhInvalido()
        {
            var id = 0;
            var usuario = new Usuario { Id = id, Email = "joao@email.com", Senha = "123456",};
            _usuarioRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(usuario);

            var resultado = _usuarioService.Obter(id, 0);

            Assert.Null(resultado);
        }

        [Fact(DisplayName = "Deve obter uma lista de usuários.")]
        public void ObterTodos_DeveRetornarListaDeUsuarios()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Email = "joao@email.com", Senha = "123456",},
                new Usuario { Id = 2, Email = "maria@email.com", Senha = "123456",},
            };
            var idUsuario = 1;
            _usuarioRepositoryMock.Setup(r => r.Obter()).ReturnsAsync(usuarios);

            var resultado = _usuarioService.ObterTodos(idUsuario).Result;

            Assert.NotNull(resultado);
            Assert.NotEmpty(resultado);
        }

        [Fact(DisplayName = "Não deve obter uma lista de usuários.")]
        public void ObterTodos_DeveRetornarListaVazia()
        {
            var idUsuario = 0;
            _usuarioRepositoryMock.Setup(r => r.Obter()).Returns(Task.FromResult(Enumerable.Empty<Usuario>()));

            var resultado = _usuarioService.ObterTodos(idUsuario).Result;;

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }
    }
}