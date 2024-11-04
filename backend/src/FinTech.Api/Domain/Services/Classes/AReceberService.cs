using AutoMapper;
using FinTech.Api.Contract.AReceber;
using FinTech.Api.Data;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Interfaces;

namespace FinTech.Api.Domain.Services.Classes
{
    public class AReceberService : ITituloService<AReceberRequestContract, AReceberResponseContract, long>
    {
        private readonly IAReceberRepository _aReceberRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ApplicationContext _contexto;

        public AReceberService(IAReceberRepository aReceberRepository, 
            IMapper mapper, 
            IUsuarioRepository usuarioRepository, 
            ApplicationContext contexto)
        {
            _aReceberRepository = aReceberRepository;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _contexto = contexto;
        }

        public async Task<AReceberResponseContract> Adicionar(AReceberRequestContract entidade, long idUsuario)
        {
            // Verificar se o Usuário existe
            var usuario = await _usuarioRepository.ObterPorId(idUsuario);

            if (usuario == null)
            {
                throw new Exception($"Usuário não encontrado com id {idUsuario}");
            }

            // Verificar se a Pessoa existe
            var pessoaRepository = new PessoaRepository(_contexto);

            var pessoa = await pessoaRepository.ObterPorId(entidade.IdPessoa);

            if (pessoa == null)
            {
                throw new Exception($"Pessoa não encontrada com id {entidade.IdPessoa}");
            }

            ValidarCamposObrigatorios(entidade);

            AReceber aReceber = _mapper.Map<AReceber>(entidade);
            aReceber.IdUsuario = idUsuario;

            aReceber = await _aReceberRepository.Adicionar(aReceber);

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        private void ValidarCamposObrigatorios(AReceberRequestContract entidade)
        {
            string mensagemErro = string.Empty;

            switch (true)
            {
                case var _ when entidade.IdPessoa <= 0:
                    mensagemErro = "O campo IdPessoa é obrigatório.";
                    break;

                case var _ when entidade.IdNaturezaLancamento <= 0:
                    mensagemErro = "O campo IdNaturezaLancamento é obrigatório.";
                    break;

                case var _ when entidade.ValorAReceber == null || entidade.ValorAReceber == 0:
                    mensagemErro = "O campo ValorAReceber é obrigatório e deve ser maior que zero.";
                    break;

                case var _ when entidade.ValorAReceber < 0:
                    mensagemErro = "O valor do título não pode ser negativo.";
                    break;
            }

            if (!string.IsNullOrEmpty(mensagemErro))
            {
                throw new Exception(mensagemErro);
            }
        }

        public async Task<AReceberResponseContract> Atualizar(long id, AReceberRequestContract entidade, long idUsuario)
        {
            AReceber aReceber = await _aReceberRepository.ObterPorId(id);

            if (aReceber == null)
                throw new KeyNotFoundException("Título a Receber não encontrado.");

            _mapper.Map(entidade, aReceber);

            // Verificar se a Pessoa existe
            var pessoaRepository = new PessoaRepository(_contexto);

            var pessoa = await pessoaRepository.ObterPorId(entidade.IdPessoa);

            if (pessoa == null)
            {
                throw new Exception($"Pessoa não encontrada com id {entidade.IdPessoa}");
            }

            ValidarCamposObrigatorios(entidade);

            aReceber.Id = aReceber.Id;
            aReceber.IdUsuario = aReceber.IdUsuario;
            aReceber.DataEmissao = aReceber.DataEmissao;

            aReceber = await _aReceberRepository.Atualizar(aReceber);

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorId(id) ?? throw new Exception("Título a Receber não encontrado para exclusão.");
            
            if (aReceber == null)
            {
                throw new Exception($"Título a Receber não encontrado com id {aReceber}");
            }

            await _aReceberRepository.Deletar(aReceber);
        }

        public async Task<IEnumerable<AReceberResponseContract>> ObterTodos(long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterTodos();

            return _mapper.Map<List<AReceberResponseContract>>(aReceber);
        }

        public async Task<AReceberResponseContract> Obter(long id, long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorId(id);

            if (aReceber is null)
            {
                throw new Exception($"Não foi encontrado nenhum título a Receber pelo id {id}");
            }

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        public async Task<AReceberResponseContract> ObterPorIdPessoa(long idPessoa)
        {
            var aReceber = await _aReceberRepository.ObterPorIdPessoa(idPessoa);

            if (aReceber is null || aReceber.IdUsuario != idPessoa)
            {
                throw new Exception($"Não foi encontrado nenhum título a Receber pelo id {idPessoa}");
            }

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        public async Task<AReceberResponseContract> ObterPorIdUsuario(long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorIdUsuario(idUsuario);

            if (aReceber is null || aReceber.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi encontrado nenhum título a Receber pelo id {idUsuario}");
            }

            return _mapper.Map<AReceberResponseContract>(aReceber);
        }

        public async Task<IEnumerable<AReceberResponseContract>> ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal, long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorPeriodo(dataInicial, dataFinal, idUsuario);
            return _mapper.Map<IEnumerable<AReceberResponseContract>>(aReceber);
        }

        public async Task<IEnumerable<AReceberResponseContract>> ObterTituloPorId(int id)
        {
            var aReceber = await _aReceberRepository.ObterPorId(id);
            return _mapper.Map<IEnumerable<AReceberResponseContract>>(aReceber);
        }

        // public async Task<AReceberResponseContract> GetAReceberByIdAsync(int id)
        // {
        //     var aReceber = await _aReceberRepository.GetByIdAsync(id);
        //     return _mapper.Map<AReceberResponseContract>(aReceber);
        // }

        // Task<AReceberResponse> ITituloService<AReceberRequestContract, AReceberResponseContract, long>.GetAReceberByIdAsync(int id)
        // {
        //     var aReceber = await _aReceberRepository.GetByIdAsync(id);
        //     return _mapper.Map<AReceberResponseContract>(aReceber);
        // }

        // Task<IEnumerable<AReceberResponseContract>> ITituloService<AReceberRequestContract, AReceberResponseContract, long>.GetAReceberByIdAsync(int id)
        // {
        //     var aReceber = await _aReceberRepository.GetByIdAsync(id);
        //     return _mapper.Map<AReceberResponseContract>(aReceber);
        // }
    }
}