using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task SaveCategoria(string nomeCategoria);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext context) : base(context) { }

        public async Task SaveCategoria(string nomeCategoria)
        {
            if(!dbSet.Where(c => c.Nome == nomeCategoria).Any())
            {
                Categoria categoria = new Categoria() { Nome = nomeCategoria };
                dbSet.Add(categoria);
                await contexto.SaveChangesAsync();
            }
        }
    }
}
