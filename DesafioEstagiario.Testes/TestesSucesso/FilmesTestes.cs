using Dominio.Models;
using Moq;
using Servicos.DTOs;
using Servicos.Interfaces;
using Servicos.RepositoryInterfaces;
using Servicos.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstagiario.Testes.Testes
{
    public class FilmesTestes
    {
        private readonly Mock<IFilmeRepository> mockFilmeRepository = new();
        private readonly Mock<IGeneroRepository> mockGeneroReposity = new();
        private readonly FilmeService filmeService;
        private readonly GeneroService generoService;

        public FilmesTestes()
        {
            filmeService = new(mockFilmeRepository.Object, mockGeneroReposity.Object);
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


            var filme = new FilmeDTO
            {
                Nome = Nome,
                AnoDeLancamento = AnoDeLancamento,
                FaixaEtaria = FaixaEtaria,
                Direcao = Direcao,
                Atores = Atores,
                Duracao = Duracao,
                Roteiristas = Roteiristas,
                GeneroId = generoIds
            };

            var listaDeGeneros = new Genero[] { new(){ Nome = "Terror", GeneroId = 1 } };

            var genero = mockGeneroReposity.Setup(g => g.GetGeneros(generoIds)).ReturnsAsync(listaDeGeneros);

            //Act
            var filmeAdicionado = await filmeService.AdicionarFilmes(filme);

            //Assert
            Assert.True(filmeAdicionado.IsSuccess);

        }

        [Fact]
        public async void AvaliarFilme()
        {
            //Arrange
            string comentario = "Muito bom";
            var usuarioId = 1;

            var avaliacoes = new AvaliacaoDTO
            {
                FilmeId = 1,
                Nota = 5,
                Comentario = comentario
            };

            //Act
            var filmeAvaliado = await filmeService.AvaliacaoFilme(avaliacoes, usuarioId);

            //Assert
            mockFilmeRepository.Verify(mf => mf.AvaliacaoFilme(filmeAvaliado));
            mockFilmeRepository.Verify(mf => mf.SaveChanges());
        }
    }
}