using AplicaçãoWebCompleta.Data.Repositoy;
using AplicaçãoWebCompleta.Data.Repositoy.Interface;
using AplicaçãoWebCompleta.Services;
using AplicaçãoWebCompleta.Services.Interface;

namespace AplicaçãoWebCompleta.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
