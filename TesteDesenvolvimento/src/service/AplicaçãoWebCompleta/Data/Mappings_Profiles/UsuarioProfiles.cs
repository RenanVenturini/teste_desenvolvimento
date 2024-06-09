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
            CreateMap<AtualizarUsuarioRequest, CadastroUsuario>();
            CreateMap<UsuarioRequest, CadastroUsuario>();
            CreateMap<CadastroUsuario, UsuarioResponse>();
        }
    }
}
