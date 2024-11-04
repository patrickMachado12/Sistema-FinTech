using FinTech.Api.Data;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTech.Api.Domain.Repository.Classes
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ApplicationContext _contexto;
        public PessoaRepository(ApplicationContext context)
        {
            _contexto = context;
        }

        public async Task<Pessoa> Adicionar(Pessoa entidade)
        {
            await _contexto.Pessoa.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<Pessoa> Atualizar(Pessoa entidade)
        {
            Pessoa entidadeBanco = _contexto.Pessoa
                                                .Where(p => p.Id == entidade.Id)
                                                .FirstOrDefault();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<Pessoa>(entidadeBanco);

            await _contexto.SaveChangesAsync();
            
            return entidadeBanco;
        }

        public async Task Deletar(Pessoa entidade)
        {
            _contexto.Entry(entidade).State = EntityState.Deleted;
            await _contexto.SaveChangesAsync();
        }

        public async Task<Pessoa> ObterPorId(long id)
        {
            return await _contexto.Pessoa.AsNoTracking()
                                                        .Where(p => p.Id == id)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<Pessoa> ObterPorNome(string nome)
        {
            return await _contexto.Pessoa.AsNoTracking()
                                                        .Where(p => p.Nome == nome)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<List<Pessoa>> ObterTodos()
        {
            return await _contexto.Pessoa.AsNoTracking().ToListAsync();
        }
    }
}