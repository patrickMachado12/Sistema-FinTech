using AutoMapper;
using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract.AReceber;
using FinTech.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinTech.Api.Controllers
{
    [ApiController]
    [Route("AReceber")]
    public class AReceberController : BaseController
    {
        private readonly IService<AReceberRequestContract, AReceberResponseContract, long> _aReceberService;
        private readonly IMapper _mapper;

        public AReceberController(
            IService<AReceberRequestContract, AReceberResponseContract, long> aReceberService)
        {
            _aReceberService = aReceberService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um título a receber.", Description = "Este endpoint efetua o cadastro de título a receber.")]
        [Authorize]
        public async Task<IActionResult> Adicionar(AReceberRequestContract contrato)
        {
        
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                
                return Created("", await _aReceberService.Adicionar(contrato, _idUsuario));
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
        [SwaggerOperation(Summary = "Obtém uma lista de títulos a Receber.", Description = "Este endpoint retorna todos os títulos a Receber cadastradas.")]
        [Authorize]
        public async Task<IActionResult> ObterTodos(long id)
        {
            try
            {
                return Ok(await _aReceberService.ObterTodos(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém um título a Receber pelo identificador.", Description = "Este endpoint retorna um título a Receber pelo seu identificador.")]
        [Route("id/{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long id, long idUsuario)
        {
            try
            {
                return Ok(await _aReceberService.Obter(id, idUsuario));
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
        [SwaggerOperation(Summary = "Atualiza um título a Receber pelo identificador.", Description = "Este endpoint atualiza um título a Receber pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, AReceberRequestContract contrato)
        {
            try
            {
                if (contrato == null)
                {
                    return BadRequest("Contrato não pode ser nulo");
                }
                
                _idUsuario = ObterIdUsuarioLogado();

                return Ok(await _aReceberService.Atualizar(id, contrato, _idUsuario));
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
        [SwaggerOperation(Summary = "Deleta um título pelo identificador.", Description = "Este endpoint deleta um título a Receber pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();

                await _aReceberService.Inativar(id, _idUsuario);

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