using AplicaçãoWebCompleta.Data.Table;

namespace AplicaçãoWebCompleta.Models.Response
{
    public class UsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public EnderecoResponse EnderecoResponse { get; set; }
    }
}
