using AplicaçãoWebCompleta.Data.Table;
using Microsoft.EntityFrameworkCore;

namespace AplicaçãoWebCompleta.Data
{
    public class AplicacaoWebContext : DbContext
    {
        public AplicacaoWebContext(DbContextOptions<AplicacaoWebContext> options) : base(options) { }

        public DbSet<CadastroUsuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CadastroUsuario>(entity =>
            {
                entity.ToTable("TbCadastroUsuario");
                entity.HasKey(x => x.UsuarioId);

                entity.HasOne(e => e.Endereco)
                      .WithOne(e => e.Usuario)
                      .HasForeignKey<Endereco>(e => e.UsuarioId);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("TbEndereco");
                entity.HasKey(x => x.EnderecoId);

                entity.HasOne(e => e.Usuario)
                      .WithOne(u => u.Endereco)
                      .HasForeignKey<Endereco>(e => e.UsuarioId)
                      .IsRequired(); 
            });
        }
    }
}