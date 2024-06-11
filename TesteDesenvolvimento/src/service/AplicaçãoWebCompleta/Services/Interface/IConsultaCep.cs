using AplicaçãoWebCompleta.Data.Table;
using Refit;
using System.Threading.Tasks;
namespace AplicaçãoWebCompleta.Services.Interface
{
    public interface IConsultaCep
    {
        [Get("/ws/{cep}/json/")]
        Task<ConsultaCep> GetCepAsync(string cep);
    }
}
