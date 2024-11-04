using FinTech.Api.Data;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTech.Api.Domain.Repository.Classes
{
    public class NaturezaLancamentoRepository : INaturezaLancamentoRepository
    {
        private readonly ApplicationContext _contexto;

        public NaturezaLancamentoRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<NaturezaLancamento> Adicionar(NaturezaLancamento entidade)
        {
            await _contexto.NaturezaLancamento.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<NaturezaLancamento> Atualizar(NaturezaLancamento entidade)
        {
            NaturezaLancamento entidadeBanco = await _contexto.NaturezaLancamento
                .Where(u => u.Id == entidade.Id)
                .FirstOrDefaultAsync();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<NaturezaLancamento>(entidadeBanco);

            await _contexto.SaveChangesAsync();

            return entidadeBanco;
        }

        public async Task Deletar(NaturezaLancamento entidade)
        {   
            // Deletar fisicamente
            _contexto.Entry(entidade).State = EntityState.Deleted;
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<NaturezaLancamento>> Obter()
        {
            // Aqui verificar uma lógica para fazer uma consulta paginada.
            return await _contexto.NaturezaLancamento.AsNoTracking()
                                            .OrderBy(u => u.Id)
                                            .ToListAsync();
        }

        public async Task<NaturezaLancamento?> Obter(long id)
        {
            return await _contexto.NaturezaLancamento.AsNoTracking()
                                                        .Where(n => n.Id == id)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NaturezaLancamento>> ObterPorIdUsuario(long idUsuario)
        {
            return await _contexto.NaturezaLancamento.AsNoTracking()
                                                        .Where(n => n.IdUsuario == idUsuario)
                                                        .OrderBy(n => n.Id)
                                                        .ToListAsync();
        }
    }
}