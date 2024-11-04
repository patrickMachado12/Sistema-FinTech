using AutoMapper;
using FinTech.Api.Contract.AReceber;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.AutoMapper
{
    public class AReceberProfile : Profile
    {
        public AReceberProfile()
        {
            CreateMap<AReceber, AReceberRequestContract>().ReverseMap();
            CreateMap<AReceber, AReceberResponseContract>().ReverseMap();
            CreateMap<Pessoa, PessoaResponse>();
            CreateMap<NaturezaLancamento, NaturezaLancamentoResponse>();
        }
    }
}