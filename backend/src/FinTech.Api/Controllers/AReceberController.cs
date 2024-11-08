using AutoMapper;
using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract;
using FinTech.Api.Contract.AReceber;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FinTech.Api.Controllers
{
    [ApiController]
    [Route("titulos-areceber")]
    public class AReceberController : BaseController
    {
        private readonly ITituloService<AReceberRequestContract, AReceberResponseContract, long> _aReceberService;
        private readonly IMapper _mapper;
        
        public AReceberController(ITituloService<AReceberRequestContract, AReceberResponseContract, long> aReceberService)
        {
            _aReceberService = aReceberService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um título a receber.", Description = "Este endpoint efetua o cadastro de título a receber.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
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

        [HttpGet("")]
        [SwaggerOperation(Summary = "Obtém uma lista de títulos a Receber.", Description = "Este endpoint retorna todos os títulos a Receber cadastradas.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
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
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ModelErrorContract))]  
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();

                await _aReceberService.Inativar(id, _idUsuario);

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
        [SwaggerOperation(Summary = "Consulta títulos a receber por período de emissão.", Description = "Este endpoint retorna os títulos a receber dentro de um período de emissão específico.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
                var aReceber = await _aReceberService.ObterPorPeriodo(dataInicial, dataFinal, _idUsuario);

                return Ok(aReceber);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}