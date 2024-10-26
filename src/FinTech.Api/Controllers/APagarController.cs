using AutoMapper;
using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinTech.Api.Controllers
{
    [ApiController]
    [Route("APagar")]
    public class APagarController : BaseController
    {
        private readonly IService<APagarRequestContract, APagarResponseContract, long> _aPagarService;
        private readonly IMapper _mapper;

        public APagarController(
            IService<APagarRequestContract, APagarResponseContract, long> aPagarService)
        {
            _aPagarService = aPagarService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um título a pagar.", Description = "Este endpoint efetua o cadastro de título a pagar.")]
        [Authorize]
        public async Task<IActionResult> Adicionar(APagarRequestContract contrato)
        {

            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _aPagarService.Adicionar(contrato, _idUsuario));
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

        [HttpGet("obterTodos")]
        [SwaggerOperation(Summary = "Obtém uma lista de títulos a pagar.", Description = "Este endpoint retorna todos os títulos a pagar cadastradas.")]
        [Authorize]
        public async Task<IActionResult> ObterTodos(long id)
        {
            try
            {
                return Ok(await _aPagarService.ObterTodos(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém um título a pagar pelo identificador.", Description = "Este endpoint retorna um título a pagar pelo seu identificador.")]
        [Route("id/{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long id, long idUsuario)
        {
            try
            {
                return Ok(await _aPagarService.Obter(id, idUsuario));
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
        [SwaggerOperation(Summary = "Atualiza um título a pagar pelo identificador.", Description = "Este endpoint atualiza um título a pagar pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, APagarRequestContract contrato)
        {
            try
            {
                if (contrato == null)
                {
                    return BadRequest("Contrato não pode ser nulo");
                }
                
                _idUsuario = ObterIdUsuarioLogado();

                return Ok(await _aPagarService.Atualizar(id, contrato, _idUsuario));
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
        [SwaggerOperation(Summary = "Deleta um título pelo identificador.", Description = "Este endpoint deleta um título a pagar pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();

                await _aPagarService.Inativar(id, _idUsuario);

                return NoContent(); // ou outro valor de retorno adequado
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