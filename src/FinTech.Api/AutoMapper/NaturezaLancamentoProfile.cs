using AutoMapper;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.AutoMapper
{
    public class NaturezaLancamentoProfile : Profile
    {
        public NaturezaLancamentoProfile()
        {
            CreateMap<NaturezaLancamento, NaturezaLancamentoRequestContract>().ReverseMap();
            CreateMap<NaturezaLancamento, NaturezaLancamentoResponseContract>().ReverseMap();
        }
    }
}