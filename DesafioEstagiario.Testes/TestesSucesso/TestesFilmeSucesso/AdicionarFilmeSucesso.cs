using Dominio.Models;
using Moq;
using Servicos.Interfaces;
using Servicos.RepositoryInterfaces;
using Servicos.UseCases.FilmeUseCases;

namespace DesafioEstagiario.Testes.TestesSucesso.TestesFilmeSucesso
{
    public class AdicionarFilmeSucesso
    {
        private readonly Mock<IFilmeRepository> mockFilmeRepository = new();
        private readonly Mock<IGeneroRepository> mockGeneroReposity = new();
        private readonly AdicionarFilmeRequestHandler adicionarFilme;

        public AdicionarFilmeSucesso()
        {
            adicionarFilme = new(mockFilmeRepository.Object, mockGeneroReposity.Object);
        }

        [Fact]
        public async Task AdicionarFilme()
        {
            //Arange
            var generoIds = new List<int> { 1 };


            const string Nome = "Anabelle";
            const string Atores = "Anabelle";
            const string AnoDeLancamento = "2020";
            const string Direcao = "Anabelle";
            const int FaixaEtaria = 18;
            const string Roteiristas = "Anabelle";
            const int Duracao = 2;


            var filme = new AdicionarFilmeRequest
            {
                Nome = Nome,
                AnoDeLancamento = AnoDeLancamento,
                FaixaEtaria = FaixaEtaria,
                Direcao = Direcao,
                Atores = Atores,
                Duracao = Duracao,
                Roteiristas = Roteiristas,
                GeneroId = generoIds,
            };

            var listaDeGeneros = new Genero[] { new() { Nome = "Terror", GeneroId = 1 } };

            var genero = mockGeneroReposity.Setup(g => g.GetGeneros(generoIds)).ReturnsAsync(listaDeGeneros);
            var cancellationToken = new CancellationToken();
            //Act
            var filmeAdicionado = await adicionarFilme.Handle(filme, cancellationToken);

            //Assert
            Assert.True(filmeAdicionado.IsSuccess);

        }
    }
}
