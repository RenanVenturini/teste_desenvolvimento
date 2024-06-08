using AplicaçãoWebCompleta.Data.Repositoy.Interface;
using AplicaçãoWebCompleta.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace AplicaçãoWebCompleta.Data.Repositoy
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AplicaçãoWebContext _context;

        public UsuarioRepository(AplicaçãoWebContext context)
        {
            _context = context;
        }

        public async Task CriarUsuarioAsync(CadastroUsuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarUsuarioAsync(CadastroUsuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CadastroUsuario>> ListarUsuarioAsync()
            => await _context.Usuarios.Include(e => e.Endereco).ToListAsync();

        public async Task<CadastroUsuario> BuscarUsuarioPorIdAsync(int id)
            => await _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(x => x.UsuarioId == id);

        public async Task RemoverUsuarioAsync(CadastroUsuario usuario)
        {
            // Verifica se o usuário possui um endereço associado a ele
            if (usuario.Endereco != null)
            {
                // Remove o endereço associado ao usuário
                _context.Enderecos.Remove(usuario.Endereco);
            }

            // Remove o usuário
            _context.Usuarios.Remove(usuario);

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
        }
    }
}
