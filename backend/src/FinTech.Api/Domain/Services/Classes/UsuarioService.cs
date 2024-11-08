using System.Net.Mail;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FinTech.Api.Contract.Usuario;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Interfaces;

namespace FinTech.Api.Domain.Services.Classes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public UsuarioService(IUsuarioRepository usuarioRepository, TokenService tokenService, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest)
        {
            UsuarioResponseContract usuario = await Obter(usuarioLoginRequest.Email);
        
            var hashSenha = GerarHashSenha(usuarioLoginRequest.Senha);

            if(usuario is null || usuario.Senha != hashSenha)
            {
                throw new AuthenticationException("Usuário e/ou senha inválido(s).");
            }

            return new UsuarioLoginResponseContract {
                Id = usuario.Id,
                Email = usuario.Email,
                Token = _tokenService.GerarToken(_mapper.Map<Usuario>(usuario))
            };        
        }

        public async Task<UsuarioResponseContract> Adicionar(UsuarioRequestContract contrato, long idUsuario)
        {
            if (!IsEmail(contrato.Email))
            {
                throw new ArgumentException("O e-mail fornecido é inválido.");
            }
            
            var usuario = _mapper.Map<Usuario>(contrato);

            usuario.Senha = GerarHashSenha(usuario.Senha);
            usuario.DataCadastro = DateTime.Now;

            usuario = await _usuarioRepository.Adicionar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        private bool IsEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UsuarioResponseContract> Atualizar(long id, UsuarioRequestContract contrato, long idUsuario)
        {
            var usuario = await _usuarioRepository.Obter(idUsuario) ?? throw new Exception("Usuario não encontrado para atualização.");

            _mapper.Map(contrato, usuario);
            usuario.Senha = GerarHashSenha(contrato.Senha);
            await _usuarioRepository.SalvarAlteracoes();
            

            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            var usuario = await _usuarioRepository.Obter(id) ?? throw new Exception("Usuario não encontrado para inativação.");
            
            await _usuarioRepository.Deletar(usuario);
        }

        public async Task<IEnumerable<UsuarioResponseContract>> ObterTodos(long idUsuario)
        {
            var usuarios = await _usuarioRepository.Obter();

            return usuarios.Select(usuario => _mapper.Map<UsuarioResponseContract>(usuario));
        }

        public async Task<UsuarioResponseContract> Obter(long id, long idUsuario)
        {
            var usuario = await _usuarioRepository.Obter(id);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public async Task<UsuarioResponseContract> Obter(string email)
        {
            var usuario = await _usuarioRepository.Obter(email);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        private string GerarHashSenha(string senha)
        {
            string hashSenha;

            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
                byte[] bytesHashSenha = sha256.ComputeHash(bytesSenha);
                hashSenha = BitConverter.ToString(bytesHashSenha).Replace("-","").Replace("-","").ToLower();
            }
            
            return hashSenha;
        }
    }
}