using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinTech.Api.Contract.Pessoa;

namespace FinTech.Api.Domain.Services.Interfaces
{
   
    public interface IPessoaService
    {
        /// <summary>
        /// Método para obter a pessoa pelo Id.
        /// </summary>
        /// <param name="id">Id da pessoa a ser obtida.</param>
        /// <returns>Retorna a entidade conforme o Id.</returns> 
        Task<PessoaResponseContract> ObterPorId(long id);

        /// <summary>
        /// Método para obter a pessoa pelo Nome.
        /// </summary>
        /// <param name="nome">Nome da pessoa a ser obtida.</param>
        /// <returns>Retorna a entidade conforme o Nome.</returns> 
        Task<PessoaResponseContract> ObterPorNome(string nome);

        /// <summary>
        /// Método para obter as pessoas cadastradas.
        /// </summary>
        /// <returns>Retorna uma lista de pessoas cadastradas.</returns> 
        Task<IList<PessoaResponseContract>> ObterTodos();

        /// <summary>
        /// Método para adicionar uma pessoa.
        /// </summary>
        /// <param name="entidade">Dados da pessoa a ser adicionada.</param>
        /// <returns>Retorna um entidade de resposta.</returns> 
        Task<PessoaResponseContract> Adicionar(PessoaRequestContract entidade);

        /// <summary>
        /// Método para atualizar uma pessoa pelo Id.
        /// </summary>
        /// <param name="id">Id da pessoa a ser atualizado.</param>
        /// <param name="entidade">Dados da pessoa a ser atualizada.</param> 
        /// <returns>Retorna um entidade de resposta.</returns> 
        Task<PessoaResponseContract> Atualizar(long id, PessoaRequestContract entidade);
        
        /// <summary>
        /// Método para inativar uma pessoa pelo Id.
        /// </summary>
        /// <param name="id">Id da pessoa a ser inativada.</param>
        /// <param name="entidade">Dados da pessoa a ser inativada.</param> 
        /// <returns>Retorna um entidade de resposta.</returns> 
        Task<PessoaResponseContract> Deletar(long id, PessoaRequestContract entidade);
    }
}