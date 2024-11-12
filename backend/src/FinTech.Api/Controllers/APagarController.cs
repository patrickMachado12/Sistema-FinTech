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
        [SwaggerOperation(
            Summary = "Cadastra um título a pagar.", 
            Description = "Este endpoint efetua o cadastro de título a pagar."
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<APagarResponseContract>> Adicionar(APagarRequestContract contrato)
        {
            _idUsuario = ObterIdUsuarioLogado();
            return await ProcessarTarefa(_aPagarService.Adicionar(contrato, _idUsuario), true);
        }

        [HttpGet("")]
        [SwaggerOperation(
            Summary = "Obtém uma lista de títulos a pagar.", 
            Description = "Este endpoint retorna todos os títulos a pagar cadastradas."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<APagarResponseContract>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<APagarResponseContract>>> ObterTodos(long id)
        {
            return await ProcessarTarefa(_aPagarService.ObterTodos(id), false);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtém um título a pagar pelo identificador.", 
            Description = "Este endpoint retorna um título a pagar pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APagarResponseContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<APagarResponseContract>> Obter(long id, long idUsuario)
        {
            return await ProcessarTarefa(_aPagarService.Obter(id, idUsuario), false);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um título a pagar pelo identificador.", 
            Description = "Este endpoint atualiza um título a pagar pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<APagarResponseContract>> Atualizar(long id, APagarRequestContract contrato)
        {
            if (contrato == null)
            {
                return BadRequest("Contrato não pode ser nulo");
            }
            
            _idUsuario = ObterIdUsuarioLogado();
            return await ProcessarTarefa(_aPagarService.Atualizar(id, contrato, _idUsuario), false);
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Deleta um título pelo identificador.", 
            Description = "Este endpoint deleta um título a pagar pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ModelErrorContract))]  
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            _idUsuario = ObterIdUsuarioLogado();
            await _aPagarService.Inativar(id, _idUsuario);
            return NoContent();
        }

        [HttpGet("periodo")]
        [SwaggerOperation(
            Summary = "Consulta títulos a pagar por período de emissão.", 
            Description = "Este endpoint retorna os títulos a pagar dentro de um período específico de emissão."
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<APagarResponseContract>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<APagarResponseContract>>> ObterPorPeriodo(
            [FromQuery] DateTime dataInicial, 
            [FromQuery] DateTime dataFinal
        )
        {
            if (dataInicial > dataFinal)
            {
                return BadRequest("A data inicial não pode ser maior que a data final.");
            }

            _idUsuario = ObterIdUsuarioLogado();
            return await ProcessarTarefa(_aPagarService.ObterPorPeriodo(dataInicial, dataFinal, _idUsuario), false);
        }
    }
}