using Dominio.Models;
using Servicos.DTOs;
using Servicos.RepositoryInterfaces;
using Servicos.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository generoRepository;
        public GeneroService(IGeneroRepository generoRepository)
        {
            this.generoRepository = generoRepository;
        }
        public async Task AddGenero(GeneroDTO genero)
        {
            var generos = new Genero
            {
                Nome = genero.Nome
            };

            generoRepository.AddGenero(generos);
            
            generoRepository.SaveChanges();
        }

        public void DeleteGenero(int id)
        {
            generoRepository.DeleteGenero(id);

            generoRepository.SaveChanges();
        }

        public IEnumerable<Genero> GenerosDisponiveis()
        {
            return generoRepository.GenerosDisponiveis();
        }

        public async Task<IEnumerable<Genero>> GetGeneros(List<int> id)
        {
            var genero = generoRepository.GetGeneros(id);

            return genero;
        }
    }
}
