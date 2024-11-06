using System.Text;
using AutoMapper;
using FinTech.Api.AutoMapper;
using FinTech.Api.Domain.Repository.Classes;
using FinTech.Api.Domain.Repository.Interfaces;
using FinTech.Api.Domain.Services.Classes;
using FinTech.Api.Domain.Services.Interfaces;
using FinTech.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Contract.AReceber;

var builder = WebApplication.CreateBuilder(args);

ConfigurarServices(builder);

ConfigurarInjecaoDeDependencia(builder);

var app = builder.Build();

ConfigurarAplicacao(app);

app.Run();

// Metodo que configrua as injeções de dependencia do projeto.
static void ConfigurarInjecaoDeDependencia(WebApplicationBuilder builder)
{
    string? connectionString = builder.Configuration.GetConnectionString("PRODUCAO");

    builder.Services.AddDbContext<ApplicationContext>(options => 
        options.UseNpgsql(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);


    var config = new MapperConfiguration(cfg => {
        
        cfg.AddProfile<UsuarioProfile>();
        cfg.AddProfile<NaturezaLancamentoProfile>();
        cfg.AddProfile<PessoaProfile>();
        cfg.AddProfile<APagarProfile>();
        cfg.AddProfile<AReceberProfile>();
        // Aqui colocar outros profiles...
    });

    IMapper mapper = config.CreateMapper();
    
    builder.Services
    .AddSingleton(builder.Configuration)
    .AddSingleton(builder.Environment)
    .AddSingleton(mapper)
    .AddScoped<TokenService>()
    .AddScoped<IUsuarioRepository, UsuarioRepository>()
    .AddScoped<IUsuarioService, UsuarioService>()
    .AddScoped<INaturezaLancamentoRepository, NaturezaLancamentoRepository>()
    .AddScoped<IService<NaturezaLancamentoRequestContract, NaturezaLancamentoResponseContract, long>, NaturezaLancamentoService>()
    .AddScoped<IPessoaRepository, PessoaRepository>()
    .AddScoped<IPessoaService, PessoaService>()
    .AddScoped<IAPagarRepository, APagarRepository>()
    .AddScoped<ITituloService<APagarRequestContract, APagarResponseContract, long>, APagarService>()
    .AddScoped<IAReceberRepository, AReceberRepository>()
    .AddScoped<ITituloService<AReceberRequestContract, AReceberResponseContract, long>, AReceberService>();

}

// Configura o serviços da API.
static void ConfigurarServices(WebApplicationBuilder builder)
{
    builder.Services
    .AddCors()
    .AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }).AddNewtonsoftJson();

    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JTW Authorization header using the Beaerer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinTech.Api", Version = "v1" });
        
        c.EnableAnnotations();
    });

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })

    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["KeySecret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

// Configura os serviços na aplicação.
static void ConfigurarAplicacao(WebApplication app)
{
    // Configura o contexto do postgreSql para usar timestamp sem time zone.
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    app.UseDeveloperExceptionPage()
        .UseRouting();

    app.UseSwagger()
        .UseSwaggerUI(c =>
        {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinTech.Api v1");
                c.RoutePrefix = string.Empty;
        });

    app.UseCors(x => x
        .AllowAnyOrigin() // Permite todas as origens
        .AllowAnyMethod() // Permite todos os métodos
        .AllowAnyHeader()) // Permite todos os cabeçalhos
        .UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints => endpoints.MapControllers());

    app.MapControllers();
}
