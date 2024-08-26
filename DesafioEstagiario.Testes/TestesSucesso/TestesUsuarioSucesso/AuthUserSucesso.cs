using Dominio.Models;
using Moq;
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
    public class AuthUserSucesso
    {
        private readonly Mock<IUserRepository> mockUserRepository = new();
        private readonly Mock<IHashService> mockHashService = new();
        private readonly Mock<ITokenService> mockTokenService = new();
        private readonly AuthUserRequestHandler authUser;
        public AuthUserSucesso()
        {
            authUser = new(mockUserRepository.Object, mockHashService.Object, mockTokenService.Object);
        }

        public async Task AuthUser()
        {
            //Arrange
            string email = "autorizacaousuario@gmail.com";
            string senha = "AuthUsuario1@";

            var cancelationToken = CancellationToken.None;
            var salt = new byte[] { 1 };

            var usuario = new Usuario
            {
                Email = email,
                Senha = senha,
                Salt = salt
            };

            var authUsuario = new AuthUserRequest
            {
                Email = email,
                Senha = senha
            };


            mockUserRepository.Setup(u => u.GetUserByEmail(email, cancelationToken)).ReturnsAsync(usuario);
            mockHashService.Setup(c => c.ComparerHashes(senha, salt, usuario.Senha)).Returns(true);


            //Act
            var cancellationToken = new CancellationToken();
            var usuarioAutorizado = await authUser.Handle(authUsuario, cancellationToken);

            //Assert
            Assert.True(usuarioAutorizado.IsSuccess);
        }
    }
}
