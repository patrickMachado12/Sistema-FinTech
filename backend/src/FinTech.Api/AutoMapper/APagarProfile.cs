using AutoMapper;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.AutoMapper
{
    public class APagarProfile : Profile
    {
        public APagarProfile()
        {
            CreateMap<APagar, APagarRequestContract>().ReverseMap();
            CreateMap<APagar, APagarResponseContract>().ReverseMap();
            CreateMap<Pessoa, PessoaResponse>();
            CreateMap<NaturezaLancamento, NaturezaLancamentoResponse>();
        }
    }
}