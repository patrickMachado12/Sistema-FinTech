using AutoMapper;
using FinTech.Api.Contract.APagar;
using FinTech.Api.Contract.NaturezaLancamento;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.AutoMapper
{
    public class APagarProfile : Profile
    {
        public APagarProfile()
        {
            CreateMap<APagar, APagarRequestContract>().ReverseMap();
            CreateMap<APagar, APagarResponseContract>()
                .ForMember(dest => dest.NaturezaLancamento, 
                    opt => opt.MapFrom(src => new NaturezaLancamentoResponseContract 
                    {
                        Id = src.NaturezaLancamento.Id,
                        Descricao = src.NaturezaLancamento.Descricao
                    }));
        }
    }
}