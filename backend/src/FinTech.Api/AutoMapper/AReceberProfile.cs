using AutoMapper;
using FinTech.Api.Contract.AReceber;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.AutoMapper
{
    public class AReceberProfile : Profile
    {
        public AReceberProfile()
        {
            CreateMap<AReceber, AReceberRequestContract>().ReverseMap();
            CreateMap<AReceber, AReceberResponseContract>()
                .ForMember(dest => dest.NaturezaLancamento, 
                    opt => opt.MapFrom(src => new NaturezaLancamentoResponseContract 
                    {
                        Id = src.NaturezaLancamento.Id,
                        Descricao = src.NaturezaLancamento.Descricao
                    }));
        }
    }
}