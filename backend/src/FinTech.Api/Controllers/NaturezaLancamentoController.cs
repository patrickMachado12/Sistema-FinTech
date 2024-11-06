using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinTech.Api.Controllers
{
    [ApiController]
    [Route("naturezaLancamento")]
    public class NaturezaLancamentoController : BaseController
    {
        private readonly IService<NaturezaLancamentoRequestContract, NaturezaLancamentoResponseContract, long> _naturezaLancamentoService;

        public NaturezaLancamentoController(
            IService<NaturezaLancamentoRequestContract, NaturezaLancamentoResponseContract, long> naturezaLancamentoService)
        {
            _naturezaLancamentoService = naturezaLancamentoService;
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra uma Natureza de Lançamento.", Description = "Este endpoint efetua o cadastro de uma Natureza de lançamento para um usuário.")]
        [Authorize]
        public async Task<IActionResult> Adicionar(NaturezaLancamentoRequestContract contrato)
        {
            try
            {  
                _idUsuario = ObterIdUsuarioLogado();
                
                return Created("", await _naturezaLancamentoService.Adicionar(contrato, _idUsuario));
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

        [HttpGet("obterTodas")]
        [SwaggerOperation(Summary = "Obtém uma lista de Naturezas de Lançamento do usuário.", Description = "Este endpoint retornas todas as Naturezas de Lançamento de um usuário.")]
        [Authorize]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaLancamentoService.ObterTodos(_idUsuario));
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

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém as Naturezas de Lançamento pelo identificador do usuário.", Description = "Este endpoint retornas todas as Naturezas de Lançamento pelo identificador do usuário.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaLancamentoService.Obter(id, _idUsuario));
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
        [SwaggerOperation(Summary = "Atualiza a Natureza de Lançamento pelo seu identificador.", Description = "Este endpoint atualiza a Natureza de Lançamento pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, NaturezaLancamentoRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaLancamentoService.Atualizar(id, contrato, _idUsuario));
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
        [SwaggerOperation(Summary = "Deleta uma Natureza de Lançamento pelo seu identificador.", Description = "Este endpoint deleta uma Natureza de Lançamento pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                await _naturezaLancamentoService.Inativar(id, _idUsuario);
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