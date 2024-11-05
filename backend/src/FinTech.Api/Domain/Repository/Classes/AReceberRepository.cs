using FinTech.Api.Data;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTech.Api.Domain.Repository.Classes
{
    public class AReceberRepository : IAReceberRepository
    {
        private readonly ApplicationContext _contexto;

        public AReceberRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<AReceber> Adicionar(AReceber entidade)
        {
            await _contexto.AReceber.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        // public async Task<AReceber> Atualizar(AReceber entidade)
        // {
        //     AReceber entidadeBanco = await _contexto.AReceber
        //         .Where(u => u.Id == entidade.Id)
        //         .FirstOrDefaultAsync();

        //     if (entidadeBanco == null)
        //     {
        //         throw new KeyNotFoundException($"AReceber com ID {entidade.Id} não encontrado.");
        //     }    

        //     _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
        //     _contexto.Update<AReceber>(entidadeBanco);

        //     await _contexto.SaveChangesAsync();

        //     return entidadeBanco;
        // }

        public async Task<AReceber> Atualizar(AReceber entidade)
        {
            AReceber entidadeBanco = await _contexto.AReceber
                .Where(u => u.Id == entidade.Id)
                .Include(a => a.Pessoa)
                .Include(a => a.NaturezaLancamento)
                .FirstOrDefaultAsync();

            if (entidadeBanco == null)
            {
                throw new KeyNotFoundException($"AReceber com ID {entidade.Id} não encontrado.");
            }    

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            await _contexto.SaveChangesAsync();

            return entidadeBanco;
        }


        public async Task Deletar(AReceber entidade)
        {
            // Deletar fisicamente
            _contexto.Entry(entidade).State = EntityState.Deleted;
            await _contexto.SaveChangesAsync();
        }

        public async Task<AReceber> ObterPorId(long id)
        {
            return await _contexto.AReceber.AsNoTracking()
                                                        .Where(n => n.Id == id)
                                                        .OrderBy(n => n.Id)
                                                        .Include(a => a.Pessoa)
                                                        .Include(a => a.NaturezaLancamento)
                                                        .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<AReceber> ObterPorIdPessoa(long idPessoa)
        {
            return await _contexto.AReceber.AsNoTracking()
                                                        .Where(n => n.IdPessoa == idPessoa)
                                                        .OrderBy(n => n.Id)
                                                        .Include(a => a.Pessoa)
                                                        .Include(a => a.NaturezaLancamento)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<AReceber> ObterPorIdUsuario(long idUsuario)
        {
            return await _contexto.AReceber.AsNoTracking()
                                                        .Where(n => n.IdUsuario == idUsuario)
                                                        .OrderBy(n => n.Id)
                                                        .Include(a => a.Pessoa)
                                                        .Include(a => a.NaturezaLancamento)
                                                        .FirstOrDefaultAsync();
        }

        // public async Task<IEnumerable<AReceber>> ObterTodos()
        // {
        //     return await _contexto.AReceber.AsNoTracking()
        //                                     .OrderBy(u => u.Id)
        //                                     .ToListAsync();
        // }

        public async Task<IEnumerable<AReceber>> ObterTodos()
        {
            return await _contexto.AReceber.AsNoTracking()
                .Include(a => a.Pessoa)
                .Include(a => a.NaturezaLancamento)
                .OrderBy(u => u.Id)
                .ToListAsync();
        }


        public async Task<IEnumerable<AReceber>> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal, long idUsuario)
        {
            return await _contexto.AReceber
                .Where(a => a.DataEmissao >= dataInicial && a.DataEmissao <= dataFinal && a.IdUsuario == idUsuario)
                .Include(a => a.Pessoa)
                .Include(a => a.NaturezaLancamento)
                .ToListAsync();
        }

        // public async Task<AReceber> GetByIdAsync(int id)
        // {
        //     return await _contexto.AReceber
        //         .Include(a => a.Pessoa)
        //         .Include(a => a.NaturezaLancamento)
        //         .FirstOrDefaultAsync(a => a.Id == id);
        // }
    }
}
