using AplicaçãoWebCompleta.Data;
using AplicaçãoWebCompleta.Data.Repositoy;
using AplicaçãoWebCompleta.Data.Table;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AplicaçãoWebCompletaTeste.Service
{
    public class UsuarioRepositoryTeste
    {
        [Fact]
        public async Task CriarUsuarioAsync()
        {
            //Arrange
            var fakeUsuario = new Faker<CadastroUsuario>()
                .RuleFor(fake => fake.Nome, "Renan")
                .RuleFor(fake => fake.Email, "renan@teste.com")
                .RuleFor(fake => fake.Telefone, "974991481")
                .Generate();

            var options = new DbContextOptionsBuilder<AplicaçãoWebContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act
                await usuarioRepository.CriarUsuarioAsync(fakeUsuario);
                var result = await context.Usuarios.FirstOrDefaultAsync(x => x.Nome == "Renan");

                //Assert
                Assert.NotNull(result);
                Assert.Equal(fakeUsuario.Nome, result.Nome);
                Assert.Equal(fakeUsuario.Email, result.Email);
                Assert.Equal(fakeUsuario.Telefone, result.Telefone);
            }
        }

        [Fact]
        public async Task AtualizarUsuarioAsync()
        {
            //Arrange
            var fakeAtualizarUsuario = new Faker<CadastroUsuario>()
                .RuleFor(fake => fake.UsuarioId, 1)
                .RuleFor(fake => fake.Nome, "Renan")
                .RuleFor(fake => fake.Email, "renan@teste.com")
                .RuleFor(fake => fake.Telefone, "974991481")
                .Generate();

            var options = new DbContextOptionsBuilder<AplicaçãoWebContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            using (var context = new AplicaçãoWebContext(options))
            {
                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 1,
                    Nome = "Jose",
                    Email = "jose@teste.com",
                    Telefone = "123456789"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act
                await usuarioRepository.AtualizarUsuarioAsync(fakeAtualizarUsuario);
                var result = await context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == 1);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(fakeAtualizarUsuario.Nome, result.Nome);
                Assert.Equal(fakeAtualizarUsuario.Email, result.Email);
                Assert.Equal(fakeAtualizarUsuario.Telefone, result.Telefone);
            }
        }

        [Fact]
        public async Task ListarUsuarioAsync()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AplicaçãoWebContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            using (var context = new AplicaçãoWebContext(options))
            {
                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 1,
                    Nome = "Jose",
                    Email = "jose@teste.com",
                    Telefone = "123456789"
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "974991481"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act
                var result = await usuarioRepository.ListarUsuarioAsync();

                var usuario1 = result.FirstOrDefault(x => x.Nome == "Jose");
                var usuario2 = result.FirstOrDefault(x => x.Nome == "Renan");

                //Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());

                Assert.NotNull(usuario1);
                Assert.Equal("Jose", usuario1.Nome);
                Assert.Equal("jose@teste.com", usuario1.Email);
                Assert.Equal("123456789", usuario1.Telefone);

                Assert.NotNull(usuario2);
                Assert.Equal("Renan", usuario2.Nome);
                Assert.Equal("renan@teste.com", usuario2.Email);
                Assert.Equal("974991481", usuario2.Telefone);
            }
        }

        [Fact]
        public async Task BuscarUsuarioPorIdAsync()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AplicaçãoWebContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            using (var context = new AplicaçãoWebContext(options))
            {
                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 1,
                    Nome = "Jose",
                    Email = "jose@teste.com",
                    Telefone = "123456789"
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "974991481"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act
                var result = await usuarioRepository.BuscarUsuarioPorIdAsync(2);

                //Assert
                Assert.NotNull(result);
                Assert.Equal("Renan", result.Nome);
                Assert.Equal("renan@teste.com", result.Email);
                Assert.Equal("974991481", result.Telefone);
            }
        }

        [Fact]
        public async Task RemoverUsuarioAsync()
        {
            //Arrange

            var options = new DbContextOptionsBuilder<AplicaçãoWebContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            using (var context = new AplicaçãoWebContext(options))
            {
                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 1,
                    Nome = "Jose",
                    Email = "jose@teste.com",
                    Telefone = "123456789"
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "974991481"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act

                var teste = await context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == 2);

                await usuarioRepository.RemoverUsuarioAsync(teste);

                var usuarioRemovido = await context.Usuarios.FindAsync(2);

                //Assert
                Assert.Null(usuarioRemovido);
            }
        }

    }
}
