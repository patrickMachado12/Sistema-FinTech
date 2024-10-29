using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract.Pessoa;
using FinTech.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinTech.Api.Controllers
{
    [ApiController]
    [Route("pessoa")]
    public class PessoaController : BaseController
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(
            IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra uma pessoa.", Description = "Este endpoint efetua o cadastro de uma Pessoa.")]
        [Authorize]
        public async Task<IActionResult> Adicionar(PessoaRequestContract contrato)
        {
            try
            {
                return Created("", await _pessoaService.Adicionar(contrato));
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
        [SwaggerOperation(Summary = "Obtém uma lista de pessoas.", Description = "Este endpoint retorna todas as pessoas cadastradas.")]
        [Authorize]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                return Ok(await _pessoaService.ObterTodos());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém uma pessoa pelo nome.", Description = "Este endpoint retorna uma pessoa pelo seu nome.")]
        [Route("nome/{nome}")]
        [Authorize]
        public async Task<IActionResult> ObterPorNome(string nome)
        {
            try
            {
                return Ok(await _pessoaService.ObterPorNome(nome));
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
        [SwaggerOperation(Summary = "Obtém uma pessoa pelo identificador.", Description = "Este endpoint retorna uma pessoa pelo seu identificador.")]
        [Route("id/{id}")]
        [Authorize]
        public async Task<IActionResult> ObterPorId(long id)
        {
            try
            {
                return Ok(await _pessoaService.ObterPorId(id));
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
        [SwaggerOperation(Summary = "Atualiza uma pessoa pelo identificador.", Description = "Este endpoint atualiza uma pessoa pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, PessoaRequestContract contrato)
        {
            try
            {
                if (contrato == null)
                {
                    return BadRequest("Contrato não pode ser nulo");
                }

                return Ok(await _pessoaService.Atualizar(id, contrato));
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
        [SwaggerOperation(Summary = "Deleta uma pessoa pelo identificador.", Description = "Este endpoint deleta uma pessoa pelo seu identificador.")]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                await _pessoaService.Deletar(id, new PessoaRequestContract());
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