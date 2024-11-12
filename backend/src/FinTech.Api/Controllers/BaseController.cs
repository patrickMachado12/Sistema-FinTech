using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ControleFacil.Api.Exceptions;
using FinTech.Api.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FinTech.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Método genérico que executa qualquer task. 
        /// </summary> 
        /// <param name="task">Task que deve ser executada.</param>
        /// <param name="isNovoCadastro">Indica se a execução da task representa um novo cadastro.</param> 
        /// <summary>
        /// <returns>retonrna o resultado da task</returns>
        protected async Task<ActionResult<T>> ProcessarTarefa<T>(Task<T> tarefa, bool isNovoCadastro = false)
        {
            try
            {
                return isNovoCadastro 
                    ? Created("", await tarefa)
                    : Ok(await tarefa);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ModelErrorContract("Not found", 404, ex.Message));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ModelErrorContract("Bad Request", 400, ex.Message));
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(new ModelErrorContract("Unauthorized", 401, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ModelErrorContract("Service Unavailable", 500, "Algo inesperado ocorreu. Erro: " + ex.Message));
            }
        }
        
        protected long _idUsuario;

        /// <summary>
        /// Obtém o ID do usuário logado com base no token JWT presente no contexto da requisição.
        /// </summary>
        /// <returns>Retorna o ID do usuário logado ou 0 se o ID não for encontrado ou inválido.</returns> 
        protected long ObterIdUsuarioLogado()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            long.TryParse(id, out long idUsuario);

            return idUsuario;
        }

    }
}