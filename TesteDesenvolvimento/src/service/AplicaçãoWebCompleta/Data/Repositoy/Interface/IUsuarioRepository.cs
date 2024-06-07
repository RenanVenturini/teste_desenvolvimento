using AplicaçãoWebCompleta.Data.Table;

namespace AplicaçãoWebCompleta.Data.Repositoy.Interface
{
    public interface IUsuarioRepository
    {
        Task CriarUsuarioAsync(CadastroUsuario usuario);
        Task AtualizarUsuarioAsync(CadastroUsuario usuario);
        Task<List<CadastroUsuario>> ListarUsuarioAsync();
        Task<CadastroUsuario> BuscarUsuarioPorIdAsync(int id);
        Task RemoverUsuarioAsync(CadastroUsuario usuario);
    }
}
