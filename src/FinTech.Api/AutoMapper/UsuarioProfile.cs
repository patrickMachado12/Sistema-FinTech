using AutoMapper;
using FinTech.Api.Contract.Usuario;
using FinTech.Api.Damain.Models;

namespace FinTech.Api.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioRequestContract>().ReverseMap();
            CreateMap<Usuario, UsuarioResponseContract>().ReverseMap();
        }
    }
}