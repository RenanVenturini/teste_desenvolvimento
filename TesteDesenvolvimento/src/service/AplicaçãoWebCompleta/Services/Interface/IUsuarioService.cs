using AplicaçãoWebCompleta.Data.Table;
using AplicaçãoWebCompleta.Models.Request;
using AplicaçãoWebCompleta.Models.Response;

namespace AplicaçãoWebCompleta.Services.Interface
{
    public interface IUsuarioService
    {
        Task CriarUsuarioAsync(UsuarioRequest usuarioRequest);
        Task AtualizarUsuarioAsync(AtualizarUsuarioRequest atualizarUsuarioRequest);
        Task<IEnumerable<UsuarioResponse>> ListarUsuarioAsync();
        Task<UsuarioResponse> BuscarUsuarioPorIdAsync(int id);
        Task RemoverUsuarioAsync(int id);
        Task<ConsultaCep> ConsultarCepAsync(string cep);
    }
}
