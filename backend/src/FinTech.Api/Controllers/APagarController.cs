using AutoMapper;
using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinTech.Api.Controllers
{
    [ApiController]
    [Route("titulos-apagar")]
    public class APagarController : BaseController
    {
        private readonly ITituloService<APagarRequestContract, APagarResponseContract, long> _aPagarService;
        private readonly IMapper _mapper;
        
        public APagarController(ITituloService<APagarRequestContract, APagarResponseContract, long> aPagarService)
        {
            _aPagarService = aPagarService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um título a pagar.", Description = "Este endpoint efetua o cadastro de título a pagar.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
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

        /// <summary>
        /// Obtém uma lista de títulos a pagar.
        /// </summary>
        /// <param name="id">O identificador do usuário para obter os títulos a pagar associados.</param>
        /// <returns>Retorna uma lista de títulos a pagar cadastrados.</returns>
        /// <response code="200">Retorna a lista de títulos a pagar.</response>
        /// <response code="401">Não autorizado. O usuário não possui permissão para acessar esse recurso.</response>
        [HttpGet("")]
        [SwaggerOperation(Summary = "Obtém uma lista de títulos a pagar.", Description = "Este endpoint retorna todos os títulos a pagar cadastradas.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<APagarResponseContract>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
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
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APagarResponseContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ModelErrorContract))]  
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();

                await _aPagarService.Inativar(id, _idUsuario);

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

        [HttpGet("periodo")]
        [SwaggerOperation(Summary = "Consulta títulos a pagar por período de emissão.", Description = "Este endpoint retorna os títulos a pagar dentro de um período específico de emissão.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<APagarResponseContract>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<IActionResult> ObterPorPeriodo([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
        {
            try
            {
                if (dataInicial > dataFinal)
                {
                    return BadRequest("A data inicial não pode ser maior que a data final.");
                }

                _idUsuario = ObterIdUsuarioLogado();
                var aPagar = await _aPagarService.ObterPorPeriodo(dataInicial, dataFinal, _idUsuario);

                return Ok(aPagar);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}