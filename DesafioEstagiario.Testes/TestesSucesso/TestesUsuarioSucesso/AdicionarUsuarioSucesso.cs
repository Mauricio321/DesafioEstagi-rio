using Moq;
using Servicos.DTOs;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;
using Servicos.UseCases.UserUseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstagiario.Testes.TestesSucesso.TestesUsuarioSucesso
{
    public class AdicionarUsuarioSucesso
    {
        private readonly Mock<IUserRepository> mockUserRepository = new();
        private readonly Mock<IHashService> mockHashService = new();
        private readonly Mock<ITokenService> mockTokenService = new();
        private readonly AdicionarUsuarioBaseRequestHandler adicionarUsuario;
        public AdicionarUsuarioSucesso()
        {
            adicionarUsuario = new(mockUserRepository.Object, mockHashService.Object);
        }

        [Fact]
        public async Task AdicionarUsuarioValido()
        {
            //Arange 
            const string NomeUsuario = "NovoUsuario";
            const string EmailValido = "novousuario@gmail.com";
            const string SenhaValida = "NovoUsuario1@";

            var usuario = new AdicionarUsuarioRequest
            {
                Nome = NomeUsuario,
                Email = EmailValido,
                Senha = SenhaValida,

            };

            mockUserRepository.Setup(u => u.UsuarioExistente(EmailValido)).ReturnsAsync(false);

            //Act
            var cancellationToken = new CancellationToken();
            var resultado = await adicionarUsuario.Handle(usuario, cancellationToken);

            //Assert
            Assert.True(resultado.IsSuccess);
            Assert.Empty(resultado.Errors);
        }
    }
}
