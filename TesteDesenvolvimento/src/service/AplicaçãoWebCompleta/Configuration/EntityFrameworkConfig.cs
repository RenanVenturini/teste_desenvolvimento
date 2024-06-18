using AplicaçãoWebCompleta.Data;
using Microsoft.EntityFrameworkCore;

namespace AplicaçãoWebCompleta.Configuration
{
    public static class EntityFrameworkConfig
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AplicacaoWebContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AplicaçãoWebConnection"),
                b => b.MigrationsAssembly(typeof(AplicacaoWebContext).Assembly.FullName))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());
        }
    }
}
