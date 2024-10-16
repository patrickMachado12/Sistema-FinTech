using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract.Usuario;
using FinTech.Api.Damain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinTech.Api.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Efetua o login do usuário.", Description = "Este endpoint retorna o token de autenticação do usuário.")]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar(UsuarioLoginRequestContract contrato)
        {
            try
            {
                return Ok(await _usuarioService.Autenticar(contrato));
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(RetornarModelUnauthorized(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um usuário.", Description = "Este endpoint cadastra um usuários na base de dados.")]
        [AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> Adicionar(UsuarioRequestContract contrato)
        {
            try
            {
                return Created("", await _usuarioService.Adicionar(contrato, 0));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém uma lista de usuários.", Description = "Este endpoint retorna todos os usuários disponíveis.")]
        [AllowAnonymous]
        public async Task<IActionResult> Obter()
        {
            try
            {
                return Ok(await _usuarioService.Obter(0));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém um usuário pelo identificador.", Description = "Este endpoint retorna um usuário pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long id)
        {
            try
            {
                return Ok(await _usuarioService.Obter(id, 0));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza um usuário pelo identificador.", Description = "Este endpoint atualiza um usuário pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, UsuarioRequestContract contrato)
        {
            try
            {
                return Ok(await _usuarioService.Atualizar(id, contrato, 0));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Deleta um usuário pelo identificador.", Description = "Este endpoint deleta um usuário pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                await _usuarioService.Inativar(id, 0);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}