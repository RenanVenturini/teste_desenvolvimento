namespace AplicaçãoWebCompleta.Data.Table
{
    public class CadastroUsuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
    }
}
