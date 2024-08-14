using Dominio.Models;
using Moq;
using Servicos.DTOs;
using Servicos.Interfaces;
using Servicos.RepositoryInterfaces;
using Servicos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioEstagiario.Testes.TestesFalha
{
    public class FilmesTestesErro
    {
        private readonly Mock<IFilmeRepository> mockFilmeRepository = new();
        private readonly Mock<IGeneroRepository> mockGeneroReposity = new();
        private readonly FilmeService filmeService;
        private readonly GeneroService generoService;

        public FilmesTestesErro()
        {
            filmeService = new(mockFilmeRepository.Object, mockGeneroReposity.Object);
        }

        [Fact]
        public async Task AdicionarFilmeErro()
        {
            //Arange
            var generoIds = new List<int> { }; //Genero nulo 


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

            var listaDeGeneros = new Genero[] { new() { Nome = "Terror", GeneroId = 1 } };

            var genero = mockGeneroReposity.Setup(g => g.GetGeneros(generoIds)).ReturnsAsync(listaDeGeneros);

            //Act
            var filmeAdicionado = await filmeService.AdicionarFilmes(filme);

            //Assert
            Assert.True(filmeAdicionado.IsSuccess);
        }
    }
}
