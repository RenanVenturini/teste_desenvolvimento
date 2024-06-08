namespace AplicaçãoWebCompleta.Data.Table
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public int UsuarioId { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public CadastroUsuario Usuario { get; set; }
    }
}
