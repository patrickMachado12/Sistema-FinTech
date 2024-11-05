using System.Threading.Tasks;
using FinTech.Api.Contract.Usuario;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Services.Interfaces;
using Xunit;

namespace FinTech.Test.DataBase
{
    public class UsuarioServiceIntegrationTests : BaseTest
    {
        [Fact]
        public async Task Deve_Criar_Usuario_Com_Sucesso()
        {
            var usuarioRequestContract = new UsuarioRequestContract
            {
                Email = "testuser@gmail.com",
                Senha = "123456"
            };

            var resultado = await _usuarioService.Adicionar(usuarioRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.Equal("testuser@gmail.com", resultado.Email);
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Por_Id()
        {
            var usuarioRequestContract = new UsuarioRequestContract
            {
                Email = "testuser@gmail.com",
                Senha = "123456"
            };

            var resultado = await _usuarioService.Adicionar(usuarioRequestContract, 1);

            Assert.NotNull(resultado);
            Assert.Equal(resultado.Id, resultado.Id);
        }

        [Fact]
        public async Task Deve_Atualizar_Usuario_Com_Sucesso()
        {
            var usuarioRequestContract = new UsuarioRequestContract
            {
                Email = "testuser@gmail.com",
                Senha = "123456"
            };

            var usuario = await _usuarioService.Adicionar(usuarioRequestContract, 1);

            var usuarioAtualizado = new UsuarioRequestContract
            {
                Email = "testeAtualizado@gmail.com",
                Senha = "123456"
            };

            await _usuarioService.Atualizar(usuario.Id, usuarioAtualizado, 1);
            var resultado = await _usuarioService.Obter(usuario.Id.ToString());

            Assert.Equal("testeAtualizado@gmail.com", resultado.Email);
        }


        [Fact]
        public async Task Deve_Deletar_Usuario_Com_Sucesso()
        {
            var usuarioRequestContract = new UsuarioRequestContract
            {
                Email = "testuser@gmail.com",
                Senha = "123456"
            };

            var usuario = await _usuarioService.Adicionar(usuarioRequestContract, 1);

            await _usuarioService.Inativar(usuario.Id, 1);
            var resultado = await _usuarioService.Obter(usuario.Id.ToString());

            Assert.Null(resultado);
        }
    }
}
