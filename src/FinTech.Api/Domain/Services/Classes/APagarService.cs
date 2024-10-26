using AutoMapper;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Interfaces;

namespace FinTech.Api.Domain.Services.Classes
{
    public class APagarService : IService<APagarRequestContract, APagarResponseContract, long>
    {
        private readonly IAPagarRepository _aPagarRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public APagarService(IAPagarRepository aPagarRepository, IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _aPagarRepository = aPagarRepository;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<APagarResponseContract> Adicionar(APagarRequestContract entidade, long idUsuario)
        {
            // Verificar se o usuário existe
            var usuario = await _usuarioRepository.ObterPorId(idUsuario);
            
            if (usuario == null)
            {
                throw new Exception($"Usuário não encontrado com id {idUsuario}");
            }
            APagar aPagar = _mapper.Map<APagar>(entidade);

            aPagar.DataEmissao = DateTime.Now;
            aPagar.IdUsuario = idUsuario;

            // Ter alguma validação para saber se tudo que eu preciso está no contrato.
            // Colocar uma validação para não poder cadastrar um título com valor negativo.

            aPagar = await _aPagarRepository.Adicionar(aPagar);

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        public async Task<APagarResponseContract> Atualizar(long id, APagarRequestContract entidade, long idUsuario)
        {
            APagar aPagar = await _aPagarRepository.ObterPorId(id);

            if (aPagar == null)
                throw new KeyNotFoundException("Título a pagar não encontrado.");

            _mapper.Map(entidade, aPagar);

            aPagar.Id = aPagar.Id;
            aPagar.IdUsuario = aPagar.IdUsuario;
            aPagar.DataEmissao = aPagar.DataEmissao;
            aPagar.DataExclusao = aPagar.DataExclusao;

            aPagar = await _aPagarRepository.Atualizar(aPagar);

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterPorId(id) ?? throw new Exception("Título a pagar não encontrado para exclusão.");
            
            if (aPagar == null)
            {
                throw new Exception($"Título a pagar não encontrado com id {aPagar}");
            }
            
            await _aPagarRepository.Deletar(aPagar);
        }

        public async Task<APagarResponseContract> Obter(long id, long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterPorId(id);

            if (aPagar is null)
            {
                throw new Exception($"Não foi encontrado nenhum título a Receber pelo id {id}");
            }

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        public async Task<APagarResponseContract> ObterPorIdPessoa(long idPessoa)
        {
            var aPagar = await _aPagarRepository.ObterPorIdPessoa(idPessoa);

            if (aPagar is null || aPagar.IdUsuario != idPessoa)
            {
                throw new Exception($"Não foi encontrado nenhum título a pagar pelo id {idPessoa}");
            }

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        public async Task<APagarResponseContract> ObterPorIdUsuario(long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterPorIdUsuario(idUsuario);

            if (aPagar is null || aPagar.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi encontrado nenhum título a pagar pelo id {idUsuario}");
            }

            return _mapper.Map<APagarResponseContract>(aPagar);
        }

        public async Task<IEnumerable<APagarResponseContract>> ObterTodos(long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterTodos();

            return _mapper.Map<List<APagarResponseContract>>(aPagar);
        }
    }
}