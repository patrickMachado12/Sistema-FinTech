using System.Security.Authentication;
using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract;
using FinTech.Api.Contract.Usuario;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinTech.Api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Efetua o login do usuário.", 
            Description = "Este endpoint retorna o token de autenticação do usuário."
        )]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioLoginResponseContract>> Autenticar(UsuarioLoginRequestContract contrato)
        {           
            return await ProcessarTarefa(_usuarioService.Autenticar(contrato), false);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra um usuário.", 
            Description = "Este endpoint cadastra um usuários na base de dados."
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioResponseContract>> Adicionar(UsuarioRequestContract contrato)
        {
            return await ProcessarTarefa(_usuarioService.Adicionar(contrato, 0), true);            
        }

        [HttpGet("")]
        [SwaggerOperation(
            Summary = "Obtém uma lista de usuários.", 
            Description = "Este endpoint retorna todos os usuários disponíveis."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioResponseContract>>> ObterTodos()
        {
           return await ProcessarTarefa(_usuarioService.ObterTodos(0), false);
            
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtém um usuário pelo identificador.", 
            Description = "Este endpoint retorna um usuário pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<UsuarioResponseContract>> Obter(long id)
        {
            return await ProcessarTarefa(_usuarioService.Obter(id, 0), false);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um usuário pelo identificador.", 
            Description = "Este endpoint atualiza um usuário pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ModelErrorContract))]   
        [Authorize]
        public async Task<ActionResult<UsuarioResponseContract>> Atualizar(long id, UsuarioRequestContract contrato)
        {
            return await ProcessarTarefa(_usuarioService.Atualizar(id, contrato, id), false);
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Deleta um usuário pelo identificador.", 
            Description = "Este endpoint deleta um usuário pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ModelErrorContract))]   
        [Authorize]
        public async Task<ActionResult<UsuarioResponseContract>> Deletar(long id)
        {
            await _usuarioService.Inativar(id, 0);
            return NoContent();
        }
    }
}