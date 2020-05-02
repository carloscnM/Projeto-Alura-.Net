using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this._categoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                await _categoriaRepository.SaveCategoria(livro.Categoria);

                var categoria = await contexto.Set<Categoria>()
                                      .Where(c => c.Nome == livro.Categoria)
                                      .SingleOrDefaultAsync();
                

                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
