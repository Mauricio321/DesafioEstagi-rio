using Moq;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;
using Servicos.UseCases.UserUseCases;

namespace DesafioEstagiario.Testes.TestesFalha12345.TesteUsuarioFalha;

public class AdicionarUsuarioBaseRequestHandlerTest
{
    private readonly Mock<IUserRepository> mockUserRepository = new();
    private readonly Mock<IHashService> mockHashService = new();
    private readonly Mock<ITokenService> mockTokenService = new();
    private readonly AdicionarUsuarioBaseRequestHandler adicionarUsuario;
    public AdicionarUsuarioBaseRequestHandlerTest()
    {
        adicionarUsuario = new(mockUserRepository.Object, mockHashService.Object);
    }

    [Theory]
    [InlineData("emailinvalido.com")] //Email sem @
    [InlineData("emailinvalido@gmail")] //Email sem .
    public async Task AdicionarUsuarioSenhaInvalida(string email)
    {
        //Arange 
        const string NomeUsuario = "NovoUsuario";

        const string SenhaValida = "NovoUsuario1@";

        var usuario = new AdicionarUsuarioRequest
        {
            Nome = NomeUsuario,
            Email = email,
            Senha = SenhaValida,

        };

        mockUserRepository.Setup(u => u.UsuarioExistente(email)).ReturnsAsync(false);

        //Act
        var cancellationToken = new CancellationToken();
        var resultado = await adicionarUsuario.Handle(usuario, cancellationToken);

        //Assert
        Assert.True(resultado.IsSuccess);
    }

    [Theory]
    [InlineData("Senha1@")] //Senha com menos de 8 caracteres 
    [InlineData("Senha123")] //Senha sem caractere especial
    [InlineData("Senha@#$")]//Senha sem numero
    [InlineData("senha123@")]//Senha sem letra maiuscula
    public async Task AdicionarUsuarioEmailInvalido(string senha)
    {
        //Arange 
        const string NomeUsuario = "NovoUsuario";
        const string EmailValido = "novousuario@gmail.com";

        var usuario = new AdicionarUsuarioRequest
        {
            Nome = NomeUsuario,
            Email = EmailValido,
            Senha = senha,

        };

        mockUserRepository.Setup(u => u.UsuarioExistente(senha)).ReturnsAsync(false);

        //Act
        var cancellationToken = new CancellationToken();
        var resultado = await adicionarUsuario.Handle(usuario, cancellationToken);

        //Assert
        Assert.True(resultado.IsFailed);
    }
}