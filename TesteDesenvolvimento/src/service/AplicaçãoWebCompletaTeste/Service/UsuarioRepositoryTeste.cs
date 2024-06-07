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
                .RuleFor(fake => fake.Cep, "09321-250")
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
                Assert.Equal(fakeUsuario.Cep, result.Cep);
            }
        }

        [Fact]
        public async Task AtualizarUsuarioAsync()
        {
            //Arrange
            var fakeAtualizarUsuario = new Faker<CadastroUsuario>()
                .RuleFor(fake => fake.Id, 1)
                .RuleFor(fake => fake.Nome, "Renan")
                .RuleFor(fake => fake.Email, "renan@teste.com")
                .RuleFor(fake => fake.Telefone, "974991481")
                .RuleFor(fake => fake.Cep, "09321-250")
                .Generate();

            var options = new DbContextOptionsBuilder<AplicaçãoWebContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            using (var context = new AplicaçãoWebContext(options))
            {
                context.Usuarios.Add(new CadastroUsuario
                {
                    Id = 1,
                    Nome = "Jose",
                    Email = "jose@teste.com",
                    Telefone = "123456789",
                    Cep = "09321-110"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act
                await usuarioRepository.AtualizarUsuarioAsync(fakeAtualizarUsuario);
                var result = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == 1);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(fakeAtualizarUsuario.Nome, result.Nome);
                Assert.Equal(fakeAtualizarUsuario.Email, result.Email);
                Assert.Equal(fakeAtualizarUsuario.Telefone, result.Telefone);
                Assert.Equal(fakeAtualizarUsuario.Cep, result.Cep);
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
                    Id = 1,
                    Nome = "Jose",
                    Email = "jose@teste.com",
                    Telefone = "123456789",
                    Cep = "09321-110"
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    Id = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "974991481",
                    Cep = "09321-250"
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
                Assert.Equal("09321-110", usuario1.Cep);

                Assert.NotNull(usuario2);
                Assert.Equal("Renan", usuario2.Nome);
                Assert.Equal("renan@teste.com", usuario2.Email);
                Assert.Equal("974991481", usuario2.Telefone);
                Assert.Equal("09321-250", usuario2.Cep);
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
                    Id = 1,
                    Nome = "Jose",
                    Email = "jose@teste.com",
                    Telefone = "123456789",
                    Cep = "09321-110"
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    Id = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "974991481",
                    Cep = "09321-250"
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
                Assert.Equal("09321-250", result.Cep);
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
                    Id = 1,
                    Nome = "Jose",
                    Email = "jose@teste.com",
                    Telefone = "123456789",
                    Cep = "09321-110"
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    Id = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "974991481",
                    Cep = "09321-250"
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act

                var teste = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == 2);

                await usuarioRepository.RemoverUsuarioAsync(teste);

                var usuarioRemovido = await context.Usuarios.FindAsync(2);

                //Assert
                Assert.Null(usuarioRemovido);
            }
        }

    }
}
