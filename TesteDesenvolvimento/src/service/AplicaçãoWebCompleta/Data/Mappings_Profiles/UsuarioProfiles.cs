using AplicaçãoWebCompleta.Data.Table;
using AplicaçãoWebCompleta.Models.Request;
using AplicaçãoWebCompleta.Models.Response;
using AutoMapper;

namespace AplicaçãoWebCompleta.Data.Mappings_Profiles
{
    public class UsuarioProfiles : Profile
    {
        public UsuarioProfiles()
        {
            CreateMap<AtualizarUsuarioRequest, CadastroUsuario>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.AtualizarEnderecoRequest));
            CreateMap<UsuarioRequest, CadastroUsuario>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.EnderecoRequest));
            CreateMap<CadastroUsuario, UsuarioResponse>()
                .ForMember(dest => dest.EnderecoResponse, opt => opt.MapFrom(src => src.Endereco));
            CreateMap<EnderecoRequest, Endereco>();
            CreateMap<Endereco, EnderecoResponse>();
            CreateMap<AtualizarEnderecoRequest, Endereco>();
        }
    }
}
