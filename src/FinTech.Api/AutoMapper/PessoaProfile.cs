using AutoMapper;
using FinTech.Api.Contract.Pessoa;
using FinTech.Api.Domain.Models;

namespace FinTech.Api.AutoMapper
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<Pessoa, PessoaRequestContract>().ReverseMap();
            CreateMap<Pessoa, PessoaResponseContract>().ReverseMap();
        }
    }
}