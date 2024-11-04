using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    public interface IAPagarRepository
    {
        Task<APagar> Adicionar(APagar entidade);
        Task<APagar> Atualizar(APagar entidade);
        Task Deletar(APagar entidade);
        Task<APagar> ObterPorId(long id);
        Task<APagar> ObterPorIdUsuario(long idUsuario);
        Task<APagar> ObterPorIdPessoa(long idPessoa);
        Task<IEnumerable<APagar>> ObterTodos();
        Task<IEnumerable<APagar>> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal, long idUsuario);
        
    }
}