using Dominio.Models;
using Moq;
using Servicos.DTOs;
using Servicos.RepositoryInterfaces;
using Servicos.Services;
using Servicos.Services.ServiceInterfaces;

namespace DesafioEstagiario.Testes.Testes;

public class UsuarioTestes
{
    private readonly Mock<IUserRepository> mockUserRepository = new();
    private readonly Mock<IHashService> mockHashService = new();
    private readonly UserService userService;
    public UsuarioTestes()
    {
        userService = new(mockUserRepository.Object, mockHashService.Object);
    }

    [Fact]
    public async Task AdicionarUsuarioValido()
    {
        //Arange 
        const string NomeUsuario = "NovoUsuario";
        const string EmailValido = "novousuario@gmail.com";
        const string SenhaValida = "NovoUsuario1@";

        var usuario = new AdministradorDTO
        {
            Nome = NomeUsuario,
            Email = EmailValido,
            Senha = SenhaValida
        };

        mockUserRepository.Setup(u => u.UsuarioExistente(EmailValido)).ReturnsAsync(false);

        //Act
        int roleIdUser = 2;
        var resultado = await userService.AddAdmin(usuario, roleIdUser);

        //Assert
        Assert.True(resultado.IsSuccess);
        Assert.Empty(resultado.Errors);
    }

    public async Task AdicaoAdminValido()
    {
        //Arange 
        const string NomeAdmin = "NovoAdmin";
        const string EmailValido = "novoadmin@gmail.com";
        const string SenhaValida = "NovoAdmin1@";

        var admin = new AdministradorDTO
        {
            Nome = NomeAdmin,
            Email = EmailValido,
            Senha = SenhaValida
        };

        mockUserRepository.Setup(u => u.UsuarioExistente(EmailValido)).ReturnsAsync(false);

        //Act
        int roleIdAdmin = 1;
        var resultado = await userService.AddAdmin(admin, roleIdAdmin);

        //Assert
        Assert.True(resultado.IsSuccess);
        Assert.Empty(resultado.Errors);
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


        mockUserRepository.Setup(u => u.GetUserByEmail(email, cancelationToken)).ReturnsAsync(usuario);
        mockHashService.Setup(c => c.ComparerHashes(senha, salt, usuario.Senha)).Returns(true);


        //Act
        var usuarioAutorizado = await userService.AuthUser(email, senha, cancelationToken);

        //Assert
        Assert.True(usuarioAutorizado.IsSuccess);

    }
}
