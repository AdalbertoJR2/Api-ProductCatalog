using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Repositories
{
    public class ProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListProductViewModel> Get()
        {
            return _context
                .Produtos
                .Include(x => x.Categoria)
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Titulo,
                    Price = x.Preco,
                    Category = x.Categoria.Titulo,
                    CategoryId = x.Categoria.Id
                })
                .AsNoTracking()
                .ToList();
        }
        public Produto Get(int id)
        {
            return _context.Produtos.Find(id);
        }
        public void Save(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }
        public void Update(Produto produto)
        {
            _context.Entry<Produto>(produto).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}