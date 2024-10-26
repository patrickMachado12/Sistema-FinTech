using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    public interface IAReceberRepository
    {
        Task<AReceber> Adicionar(AReceber entidade);
        Task<AReceber> Atualizar(AReceber entidade);
        Task Deletar(AReceber entidade);
        Task<AReceber> ObterPorId(long id);
        Task<AReceber> ObterPorIdUsuario(long idUsuario);
        Task<AReceber> ObterPorIdPessoa(long idPessoa);
        Task<IEnumerable<AReceber>> ObterTodos();
        
    }
}