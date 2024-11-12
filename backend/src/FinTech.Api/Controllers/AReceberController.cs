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
        [SwaggerOperation(
            Summary = "Cadastra um título a receber.", 
            Description = "Este endpoint efetua o cadastro de título a receber."
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<AReceberResponseContract>> Adicionar(AReceberRequestContract contrato)
        {   
            _idUsuario = ObterIdUsuarioLogado();       
            return await ProcessarTarefa(_aReceberService.Adicionar(contrato, _idUsuario), true);
        }

        [HttpGet("")]
        [SwaggerOperation(
            Summary = "Obtém uma lista de títulos a Receber.", 
            Description = "Este endpoint retorna todos os títulos a Receber cadastradas."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AReceberResponseContract>>> ObterTodos(long id)
        {

            return await ProcessarTarefa(_aReceberService.ObterTodos(id), false);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtém um título a Receber pelo identificador.", 
            Description = "Este endpoint retorna um título a Receber pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<AReceberResponseContract>> Obter(long id, long idUsuario)
        {
            return await ProcessarTarefa(_aReceberService.Obter(id, idUsuario), false);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um título a Receber pelo identificador.", 
            Description = "Este endpoint atualiza um título a Receber pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<AReceberResponseContract>> Atualizar(long id, AReceberRequestContract contrato)
        {
            if (contrato == null)
            {
                return BadRequest("Contrato não pode ser nulo");
            }
            _idUsuario = ObterIdUsuarioLogado();
            return await ProcessarTarefa(_aReceberService.Atualizar(id, contrato, _idUsuario), false);
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Deleta um título pelo identificador.", 
            Description = "Este endpoint deleta um título a Receber pelo seu identificador."
        )]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(ModelErrorContract))]  
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            _idUsuario = ObterIdUsuarioLogado();
            await _aReceberService.Inativar(id, _idUsuario);
            return NoContent();
        }

        [HttpGet("periodo")]
        [SwaggerOperation(Summary = "Consulta títulos a receber por período de emissão.", Description = "Este endpoint retorna os títulos a receber dentro de um período de emissão específico.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ModelErrorContract))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type=typeof(ModelErrorContract))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AReceberResponseContract>>> ObterPorPeriodo([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
        {
            if (dataInicial > dataFinal)
            {
                return BadRequest("A data inicial não pode ser maior que a data final.");
            }

            _idUsuario = ObterIdUsuarioLogado();
            return await ProcessarTarefa(_aReceberService.ObterPorPeriodo(dataInicial, dataFinal, _idUsuario), false);
        }

    }
}