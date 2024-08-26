//using Dominio.Models;
//using Moq;
//using Servicos.Interfaces;
//using Servicos.RepositoryInterfaces;
//using Servicos.UseCases.FilmeUseCases;

//namespace DesafioEstagiario.Testes.TestesSucesso.TestesFilmeSucesso
//{
//    public class AvaliarFilmeRequest
//    {
//        private readonly Mock<IFilmeRepository> mockFilmeRepository = new();
//        private readonly Mock<IGeneroRepository> mockGeneroReposity = new();
//        private readonly AvaliacaoFilmeRequestHandler adicionarAvaliacaoFilme;

//        public AvaliarFilmeRequest()
//        {
//            adicionarAvaliacaoFilme = new(mockFilmeRepository.Object);
//        }

//        [Fact]
//        public async void QuandoAvaliacaoNaoExiste_UmaNovaAvaliacaoEhAdicionada()
//        {
//            //Arrange
//            string comentario = "Muito bom";
//            var usuarioId = 1;

//            var avaliacoes = new AvaliacaoFilmeRequest
//            {
//                FilmeId = 1,
//                Nota = 5,
//                Comentario = comentario,
//                UsuarioId = usuarioId,
//            };

//            //Act
//            var cancellationToken = new CancellationToken();

//            var filmeAvaliado = await adicionarAvaliacaoFilme.Handle(avaliacoes, cancellationToken);

//            //Assert
//            mockFilmeRepository.Verify(mf => mf.AvaliacaoFilme(filmeAvaliado));
//            mockFilmeRepository.Verify(mf => mf.SaveChangesAsync());
//        }

//        [Fact]
//        public async void QuandoAvaliacaoJaExiste_AtualizaAAvaliacaoExistente()
//        {
//            //Arrange
//            string comentario = "Muito bom";
//            var usuarioId = 1;

//            var request = new AvaliacaoFilmeRequest
//            {
//                FilmeId = 1,
//                Nota = 3,
//                Comentario = comentario,
//                UsuarioId = usuarioId,
//            };

//            var filmeAvaliado = new Avaliacao { FilmeId = 1, UsuarioId = 1, Comentario = comentario, Nota = 5 };

//            //Act
//            mockFilmeRepository.Setup(u => u.UsuarioJaAvaliou(request.UsuarioId, request.FilmeId)).ReturnsAsync(filmeAvaliado);


//            var cancellationToken = new CancellationToken();

//            var result = await adicionarAvaliacaoFilme.Handle(request, cancellationToken);

//            //Assert
//            mockFilmeRepository.Verify(mf => mf.SaveChangesAsync());
//            mockFilmeRepository.Verify(u => u.AtualizarAvaliacao(filmeAvaliado));
//            Assert.Equal(request.Nota, filmeAvaliado.Nota);
//        }
//    }
//}
