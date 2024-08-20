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
    public class AdicionarAdminSucesso
    {
        private readonly Mock<IUserRepository> mockUserRepository = new();
        private readonly Mock<IHashService> mockHashService = new();
        private readonly Mock<ITokenService> mockTokenService = new();
        private readonly AdicionarUsuarioBaseRequestHandler adicionarAdmin;
        public AdicionarAdminSucesso()
        {
            adicionarAdmin = new(mockUserRepository.Object, mockHashService.Object);
        }

        public async Task AdicaoAdminValido()
        {
            //Arange 
            const string NomeAdmin = "NovoAdmin";
            const string EmailValido = "novoadmin@gmail.com";
            const string SenhaValida = "NovoAdmin1@";

            var admin = new AdicionarAdministradorRequest
            {
                Nome = NomeAdmin,
                Email = EmailValido,
                Senha = SenhaValida
            };

            mockUserRepository.Setup(u => u.UsuarioExistente(EmailValido)).ReturnsAsync(false);

            //Act
            var cancellationToken = new CancellationToken();
            var resultado = await adicionarAdmin.Handle(admin, cancellationToken);

            //Assert
            Assert.True(resultado.IsSuccess);
            Assert.Empty(resultado.Errors);
        }
    }
}
