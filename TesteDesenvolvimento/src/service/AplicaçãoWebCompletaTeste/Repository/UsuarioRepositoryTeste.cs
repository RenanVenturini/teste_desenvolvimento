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

namespace AplicaçãoWebCompletaTeste.Repository
{
    public class UsuarioRepositoryTeste
    {
        [Fact]
        public async Task CriarUsuarioAsync()
        {
            // Arrange
            var fakeEndereco = new Faker<Endereco>()
                .RuleFor(fake => fake.CEP, "123456789")
                .RuleFor(fake => fake.Rua, "Projetada")
                .RuleFor(fake => fake.Numero, "118")
                .RuleFor(fake => fake.Complemento, "Casa")
                .RuleFor(fake => fake.Bairro, "Jardim")
                .RuleFor(fake => fake.Cidade, "Mauá")
                .RuleFor(fake => fake.UF, "SP");

            var fakeUsuarioFaker = new Faker<CadastroUsuario>()
                .RuleFor(fake => fake.Nome, "Renan")
                .RuleFor(fake => fake.Email, "renan@teste.com")
                .RuleFor(fake => fake.Telefone, "974991481")
                .RuleFor(fake => fake.Endereco, () => fakeEndereco.Generate());

            var fakeUsuario = fakeUsuarioFaker.Generate();

            var options = new DbContextOptionsBuilder<AplicaçãoWebContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                // Act
                await usuarioRepository.CriarUsuarioAsync(fakeUsuario);
                var result = await context.Usuarios.Include(e => e.Endereco)
                                                   .FirstOrDefaultAsync(x => x.Nome == "Renan");

                // Assert
                Assert.NotNull(result);
                Assert.Equal(fakeUsuario.Nome, result.Nome);
                Assert.Equal(fakeUsuario.Email, result.Email);
                Assert.Equal(fakeUsuario.Telefone, result.Telefone);

                Assert.NotNull(result.Endereco);
                Assert.Equal(fakeUsuario.Endereco.CEP, result.Endereco.CEP);
                Assert.Equal(fakeUsuario.Endereco.Rua, result.Endereco.Rua);
                Assert.Equal(fakeUsuario.Endereco.Numero, result.Endereco.Numero);
                Assert.Equal(fakeUsuario.Endereco.Complemento, result.Endereco.Complemento);
                Assert.Equal(fakeUsuario.Endereco.Bairro, result.Endereco.Bairro);
                Assert.Equal(fakeUsuario.Endereco.Cidade, result.Endereco.Cidade);
                Assert.Equal(fakeUsuario.Endereco.UF, result.Endereco.UF);
            }
        }

        [Fact]
        public async Task AtualizarUsuarioAsync()
        {
            //Arrange
            var fakeEndereco = new Faker<Endereco>()
                .RuleFor(fake => fake.UsuarioId, 1)
                .RuleFor(fake => fake.CEP, "123456789")
                .RuleFor(fake => fake.Rua, "Projetada")
                .RuleFor(fake => fake.Numero, "118")
                .RuleFor(fake => fake.Complemento, "Casa")
                .RuleFor(fake => fake.Bairro, "Jardim")
                .RuleFor(fake => fake.Cidade, "Mauá")
                .RuleFor(fake => fake.UF, "SP");

            var fakeAtualizarUsuarioFaker = new Faker<CadastroUsuario>()
                .RuleFor(fake => fake.UsuarioId, 1)
                .RuleFor(fake => fake.Nome, "Renan")
                .RuleFor(fake => fake.Email, "renan@teste.com")
                .RuleFor(fake => fake.Telefone, "974991481")
                .RuleFor(fake => fake.Endereco, () => fakeEndereco.Generate());

            var fakeAtualizarUsuario = fakeAtualizarUsuarioFaker.Generate();

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
                    Telefone = "123456789",
                    Endereco = new Endereco
                    {
                        CEP = "123456789",
                        Rua = "Projetada",
                        Numero = "100",
                        Complemento = "Casa",
                        Bairro = "Jardim",
                        Cidade = "Santo André",
                        UF = "SP"
                    }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act
                await usuarioRepository.AtualizarUsuarioAsync(fakeAtualizarUsuarioFaker);
                var result = await context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == 1);

                //Assert
                Assert.NotNull(result);
                Assert.Equal(fakeAtualizarUsuario.Nome, result.Nome);
                Assert.Equal(fakeAtualizarUsuario.Email, result.Email);
                Assert.Equal(fakeAtualizarUsuario.Telefone, result.Telefone);

                Assert.NotNull(result.Endereco);
                Assert.Equal(fakeAtualizarUsuario.Endereco.CEP, result.Endereco.CEP);
                Assert.Equal(fakeAtualizarUsuario.Endereco.Rua, result.Endereco.Rua);
                Assert.Equal(fakeAtualizarUsuario.Endereco.Numero, result.Endereco.Numero);
                Assert.Equal(fakeAtualizarUsuario.Endereco.Complemento, result.Endereco.Complemento);
                Assert.Equal(fakeAtualizarUsuario.Endereco.Bairro, result.Endereco.Bairro);
                Assert.Equal(fakeAtualizarUsuario.Endereco.Cidade, result.Endereco.Cidade);
                Assert.Equal(fakeAtualizarUsuario.Endereco.UF, result.Endereco.UF);
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
                    Telefone = "123456789",
                    Endereco = new Endereco
                    {
                        CEP = "123456789",
                        Rua = "Projetada",
                        Numero = "100",
                        Complemento = "Casa",
                        Bairro = "Jardim",
                        Cidade = "Santo André",
                        UF = "SP"
                    }
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "123456789",
                    Endereco = new Endereco
                    {
                        CEP = "111111111",
                        Rua = "Sofia",
                        Numero = "118",
                        Complemento = "Casa",
                        Bairro = "Maringa",
                        Cidade = "Mauá",
                        UF = "SP"
                    }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act
                var result = await usuarioRepository.ListarUsuarioAsync();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());

                var usuario1 = result.FirstOrDefault(x => x.Nome == "Jose");
                Assert.NotNull(usuario1);
                Assert.Equal("Jose", usuario1.Nome);
                Assert.Equal("jose@teste.com", usuario1.Email);
                Assert.Equal("123456789", usuario1.Telefone);

                Assert.NotNull(usuario1.Endereco);
                Assert.Equal("123456789", usuario1.Endereco.CEP);
                Assert.Equal("Projetada", usuario1.Endereco.Rua);
                Assert.Equal("100", usuario1.Endereco.Numero);
                Assert.Equal("Casa", usuario1.Endereco.Complemento);
                Assert.Equal("Jardim", usuario1.Endereco.Bairro);
                Assert.Equal("Santo André", usuario1.Endereco.Cidade);
                Assert.Equal("SP", usuario1.Endereco.UF);

                var usuario2 = result.FirstOrDefault(x => x.Nome == "Renan");
                Assert.NotNull(usuario2);
                Assert.Equal("Renan", usuario2.Nome);
                Assert.Equal("renan@teste.com", usuario2.Email);
                Assert.Equal("123456789", usuario2.Telefone);

                Assert.NotNull(usuario2.Endereco);
                Assert.Equal("111111111", usuario2.Endereco.CEP);
                Assert.Equal("Sofia", usuario2.Endereco.Rua);
                Assert.Equal("118", usuario2.Endereco.Numero);
                Assert.Equal("Casa", usuario2.Endereco.Complemento);
                Assert.Equal("Maringa", usuario2.Endereco.Bairro);
                Assert.Equal("Mauá", usuario2.Endereco.Cidade);
                Assert.Equal("SP", usuario2.Endereco.UF);
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
                    Telefone = "123456789",
                    Endereco = new Endereco
                    {
                        CEP = "123456789",
                        Rua = "Projetada",
                        Numero = "100",
                        Complemento = "Casa",
                        Bairro = "Jardim",
                        Cidade = "Santo André",
                        UF = "SP"
                    }
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "123456789",
                    Endereco = new Endereco
                    {
                        CEP = "111111111",
                        Rua = "Sofia",
                        Numero = "118",
                        Complemento = "Casa",
                        Bairro = "Maringa",
                        Cidade = "Mauá",
                        UF = "SP"
                    }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new AplicaçãoWebContext(options))
            {
                var usuarioRepository = new UsuarioRepository(context);

                //Act
                var result = await usuarioRepository.BuscarUsuarioPorIdAsync(2);

                Assert.NotNull(result);
                Assert.Equal("Renan", result.Nome);
                Assert.Equal("renan@teste.com", result.Email);
                Assert.Equal("123456789", result.Telefone);

                Assert.NotNull(result.Endereco);
                Assert.Equal("111111111", result.Endereco.CEP);
                Assert.Equal("Sofia", result.Endereco.Rua);
                Assert.Equal("118", result.Endereco.Numero);
                Assert.Equal("Casa", result.Endereco.Complemento);
                Assert.Equal("Maringa", result.Endereco.Bairro);
                Assert.Equal("Mauá", result.Endereco.Cidade);
                Assert.Equal("SP", result.Endereco.UF);
            }
        }

        [Fact]
        public async Task RemoverUsuarioAsync()
        {
            // Arrange
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
                    Telefone = "123456789",
                    Endereco = new Endereco
                    {
                        CEP = "123456789",
                        Rua = "Projetada",
                        Numero = "100",
                        Complemento = "Casa",
                        Bairro = "Jardim",
                        Cidade = "Santo André",
                        UF = "SP"
                    }
                });

                context.Usuarios.Add(new CadastroUsuario
                {
                    UsuarioId = 2,
                    Nome = "Renan",
                    Email = "renan@teste.com",
                    Telefone = "123456789",
                    Endereco = new Endereco
                    {
                        CEP = "111111111",
                        Rua = "Sofia",
                        Numero = "118",
                        Complemento = "Casa",
                        Bairro = "Maringa",
                        Cidade = "Mauá",
                        UF = "SP"
                    }
                });

                await context.SaveChangesAsync();

                // Act
                var usuarioRepository = new UsuarioRepository(context);
                var usuario = await context.Usuarios.FindAsync(1);

                await usuarioRepository.RemoverUsuarioAsync(usuario);

                var usuarioRemovido = await context.Usuarios.FindAsync(1);
                var enderecoRemovido = await context.Enderecos.FindAsync(1);

                // Assert
                Assert.Null(usuarioRemovido);
                Assert.Null(enderecoRemovido);
            }
        }
    }
}
