namespace AplicaçãoWebCompleta.Models.Response
{
    public class EnderecoResponse
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
    }
}
