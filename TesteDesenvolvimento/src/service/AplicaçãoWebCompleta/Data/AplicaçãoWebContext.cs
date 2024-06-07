using AplicaçãoWebCompleta.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace AplicaçãoWebCompleta.Data
{
    public class AplicaçãoWebContext : DbContext
    {
        public AplicaçãoWebContext(DbContextOptions<AplicaçãoWebContext> options) : base(options) { }

        public DbSet<CadastroUsuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CadastroUsuario>(entity =>
            {
                entity.ToTable("TbCadastroUsuario");

                entity.HasKey(x => x.Id);
            });
        }
    }
}