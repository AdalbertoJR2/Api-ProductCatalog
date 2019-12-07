using System;

namespace ProductCatalog.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }

        public string Imagem { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdate { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
    }
}