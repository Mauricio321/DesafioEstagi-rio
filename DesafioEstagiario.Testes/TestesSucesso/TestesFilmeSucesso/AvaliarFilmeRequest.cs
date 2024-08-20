using Moq;
using Servicos.DTOs;
using Servicos.Interfaces;
using Servicos.RepositoryInterfaces;
using Servicos.UseCases.FilmeUseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstagiario.Testes.TestesSucesso.TestesFilmeSucesso
{
    public class AvaliarFilmeRequest
    {
        private readonly Mock<IFilmeRepository> mockFilmeRepository = new();
        private readonly Mock<IGeneroRepository> mockGeneroReposity = new();
        private readonly AvaliacaoFilmeRequestHandler avaliacaoFilme;

        public AvaliarFilmeRequest()
        {
            avaliacaoFilme = new(mockFilmeRepository.Object);
        }

        [Fact]
        public async void AvaliarFilme()
        {
            //Arrange
            string comentario = "Muito bom";
            var usuarioId = 1;

            var avaliacoes = new AvaliacaoFilmeRequest
            {
                FilmeId = 1,
                Nota = 5,
                Comentario = comentario,
                UsuarioId = usuarioId,
            };

            //Act
            var cancellationToken = new CancellationToken();

            var filmeAvaliado = await avaliacaoFilme.Handle(avaliacoes, cancellationToken);

            //Assert
            mockFilmeRepository.Verify(mf => mf.AvaliacaoFilme(filmeAvaliado));
            mockFilmeRepository.Verify(mf => mf.SaveChangesAsync());
        }
    }
}
