using Dominio.Models;
using Moq;
using Servicos.DTOs;
using Servicos.RepositoryInterfaces;
using Servicos.Services;
using Servicos.Services.ServiceInterfaces;

namespace DesafioEstagiario.Testes.TestesFalha;

public class UsuariosTestesErro
{
    private readonly Mock<IUserRepository> mockUserRepository = new();
    private readonly Mock<IHashService> mockHashService = new();
    private readonly Mock<ITokenService> mockTokenService = new();
    private readonly UserService userService;
    public UsuariosTestesErro()
    {
        userService = new(mockUserRepository.Object, mockHashService.Object, mockTokenService.Object);
    }

    [Fact]
    public async Task AdicionarUsuarioInvalido()
    {
        //Arange 
        const string NomeUsuario = "NovoUsuario";
        const string EmailValido = "novousuariogmail.com"; //email sem @
        const string SenhaValida = "NovoUsuario@"; // Senha sem numero

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

    [Fact]
    public async Task AdicionarAdminInvalido()
    {
        //Arange 
        const string NomeAdmin = "NovoAdmin";
        const string EmailValido = "novoadmingmail.com"; //email sem @
        const string SenhaValida = "Novoadmin@"; // Senha sem numero

        var admin = new AdministradorDTO
        {
            Nome = NomeAdmin,
            Email = EmailValido,
            Senha = SenhaValida
        };

        mockUserRepository.Setup(u => u.UsuarioExistente(EmailValido)).ReturnsAsync(false);

        //Act
        int roleIdUser = 1;
        var resultado = await userService.AddAdmin(admin, roleIdUser);

        //Assert
        Assert.True(resultado.IsSuccess);
        Assert.Empty(resultado.Errors);
    }

    public async Task AuthUserErro()
    {
        //Arrange
        string email = "autorizacaousuario@gmail.com";
        string senha = "AuthUsuario1@";

        string outroEmail = "emaildiferente@gmail.com";
        string outraSenha = "SenhaDiferente1@";

        var cancelationToken = CancellationToken.None;
        var salt = new byte[] { 1 };

        var usuario = new Usuario
        {
            Email = outroEmail,
            Senha = outraSenha,
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
