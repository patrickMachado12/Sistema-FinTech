using FinTech.Api.Domain.Models;

namespace FinTech.Api.Domain.Repository.Interfaces
{
    public interface INaturezaLancamentoRepository : IRepository<NaturezaLancamento, long>
    {
        Task<IEnumerable<NaturezaLancamento>> ObterPorIdUsuario(long idUsuario);
    }
}