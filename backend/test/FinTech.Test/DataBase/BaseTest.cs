using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinTech.Api.AutoMapper;
using FinTech.Api.Data;
using FinTech.Api.Domain.Models;
using FinTech.Api.Domain.Repository.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace FinTech.Test.DataBase
{
    public abstract class BaseTest
    {
        protected ApplicationContext _context;
        protected IConfiguration configuration;
        protected IMapper _mapper;
        protected TokenService _tokenService;
        protected IUsuarioRepository _usuarioRepository;
        protected IUsuarioService _usuarioService;
        protected IPessoaRepository _pessoaRepository;
        protected IPessoaService _pessoaService;
        protected IAPagarRepository _aPagarRepository;
        protected AReceberRepository _aReceberRepository;
        protected INaturezaLancamentoRepository _naturezaLancamentoRepository;
        protected NaturezaLancamentoService _naturezaLancamentoService;


        protected string emailUsuarioAutenticado = "admin@gmail.com";

        private readonly string _connectionString = "User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=FinTech;Pooling=true;";

        public BaseTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UsuarioProfile>();
                cfg.AddProfile<PessoaProfile>();
                cfg.AddProfile<APagarProfile>();
                cfg.AddProfile<AReceberProfile>();
                cfg.AddProfile<NaturezaLancamentoProfile>();
            });

            _mapper = config.CreateMapper();

            var contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql(_connectionString)
                .Options;

            _context = new ApplicationContext(contextOptions);
            configuration = new ConfigurationBuilder().Build();
            _tokenService = new TokenService(configuration);
            _usuarioRepository = new UsuarioRepository(_context);
            _usuarioService = new UsuarioService(_usuarioRepository, _tokenService, _mapper);
            _pessoaRepository = new PessoaRepository(_context);
            _pessoaService = new PessoaService(_pessoaRepository, _mapper);
            _aPagarRepository = new APagarRepository(_context);
            _aReceberRepository = new AReceberRepository(_context);
            _naturezaLancamentoRepository = new NaturezaLancamentoRepository(_context);
            _naturezaLancamentoService = new NaturezaLancamentoService(_naturezaLancamentoRepository, _mapper);
        }
    }
}