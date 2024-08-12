using Dominio.Models;
using Servicos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Services.ServiceInterfaces
{
    public interface IGeneroService
    {
        Task AddGenero(GeneroDTO genero);
        Task<IEnumerable<Genero>> GetGeneros(List<int> id);
        void DeleteGenero(int id);
        Task<IEnumerable<Genero>> GenerosDisponiveis();
    }
}
