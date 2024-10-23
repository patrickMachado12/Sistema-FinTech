using AutoMapper;
using FinTech.Api.Contract.Pessoa;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Interfaces;

namespace FinTech.Api.Domain.Services.Classes
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public PessoaService(
            IPessoaRepository pessoaRepository,
            IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        public async Task<PessoaResponseContract> Adicionar(PessoaRequestContract entidade)
        {
            var pessoa = _mapper.Map<Pessoa>(entidade);

            entidade.Nome = pessoa.Nome;
            entidade.Telefone = pessoa.Telefone;
            entidade.DataCadastro = DateTime.Now;

            pessoa = await _pessoaRepository.Adicionar(pessoa);

            return _mapper.Map<PessoaResponseContract>(pessoa);
        }

        public async Task<PessoaResponseContract> Atualizar(long id, PessoaRequestContract entidade)
        {
            var pessoa = await _pessoaRepository.ObterPorId(id);
            if (pessoa == null)
            {
                throw new Exception("Pessoa não encontrado para atualização.");
            }

            pessoa.Nome = entidade.Nome;
            pessoa.Telefone = entidade.Telefone;

            pessoa = await _pessoaRepository.Atualizar(pessoa);

            return _mapper.Map<PessoaResponseContract>(pessoa);

        }

        public async Task<PessoaResponseContract> Deletar(long id, PessoaRequestContract entidade)
        {
            var pessoa = await _pessoaRepository.ObterPorId(id) ?? throw new Exception("Pessoa não encontrado para inativação.");
            
            await _pessoaRepository.Deletar(_mapper.Map<Pessoa>(pessoa));

            return _mapper.Map<PessoaResponseContract>(pessoa);
        }

        public async Task<PessoaResponseContract> ObterPorId(long id)
        {
            var pessoa = await _pessoaRepository.ObterPorId(id);

            return _mapper.Map<PessoaResponseContract>(pessoa);
        }

        public async Task<PessoaResponseContract> ObterPorNome(string nome)
        {
            var pessoa = await _pessoaRepository.ObterPorNome(nome);

            return _mapper.Map<PessoaResponseContract>(pessoa);
        }

        public async Task<IList<PessoaResponseContract>> ObterTodos()
        {
            var pessoas = await _pessoaRepository.ObterTodos();

            return _mapper.Map<List<PessoaResponseContract>>(pessoas);
        }
    }
}