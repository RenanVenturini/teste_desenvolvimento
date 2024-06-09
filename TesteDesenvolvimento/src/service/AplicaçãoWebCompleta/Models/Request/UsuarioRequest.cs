using AplicaçãoWebCompleta.Data.Table;

namespace AplicaçãoWebCompleta.Models.Request
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public EnderecoRequest EnderecoRequest { get; set; }
    }
}
