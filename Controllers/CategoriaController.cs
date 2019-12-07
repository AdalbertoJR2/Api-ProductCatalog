using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly StoreDataContext _context;

        public CategoriaController(StoreDataContext context)
        {
            _context = context;
        }

        [Route("v1/categorias")]
        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public IEnumerable<Categoria> Get()
        {
            return _context.Categorias.AsNoTracking().ToList();
        }

        [Route("v1/categorias/{id}")]
        [HttpGet]
        public Categoria Get(int id)
        {
            // Find() ainda não suporta AsNoTracking
            return _context.Categorias.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/categorias/{id}/produtos")]
        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IEnumerable<Produto> GetProducts(int id)
        {
            return _context.Produtos.AsNoTracking().Where(x => x.CategoriaId == id).ToList();
        }

        [Route("v1/categorias")]
        [HttpPost]
        public Categoria Post([FromBody]Categoria category)
        {
            _context.Categorias.Add(category);
            _context.SaveChanges();

            return category;
        }

        [Route("v1/categorias")]
        [HttpPut]
        public Categoria Put([FromBody]Categoria categoria)
        {
            _context.Entry<Categoria>(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return categoria;
        }

        [Route("v1/categorias")]
        [HttpDelete]
        public Categoria Delete([FromBody]Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return categoria;
        }
    }
}