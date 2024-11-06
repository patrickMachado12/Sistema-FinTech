using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinTech.Api.Domain.Services.Interfaces
{
        /// <summary>
    /// Interface generica para criação de serviços do tipo CRUD.
    /// </summary>
    /// <typeparam name="RQ">Contrato de request</typeparam>
    /// <typeparam name="RS">Contrato de response</typeparam>
    /// <typeparam name="I">Tipo do Id</typeparam>
    public interface ITituloService<RQ, RS, I> where RQ : class
    {
        Task<IEnumerable<RS>> ObterTodos(I idUsuario);
        Task<RS> Obter(I id, I idUsuario);
        Task<RS> Adicionar(RQ entidade, I idUsuario);
        Task<RS> Atualizar(I id, RQ entidade, I idUsuario);
        Task Inativar(I id, I idUsuario);
        Task<IEnumerable<RS>> ObterPorPeriodo(DateTime inicio, DateTime fim, I idUsuario);
        //Task<AReceberResponse> GetAReceberByIdAsync(int id);
        Task<IEnumerable<RS>> ObterTituloPorId(int id);
      
    }
}