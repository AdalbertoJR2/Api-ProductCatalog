using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels.ProductViewModels;
using ProductCatalog.ViewModels.ProdutoViewModels;

namespace ProdutoCatalog.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProductRepository _repository;

        public ProdutoController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/produtos")]
        [HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/produtos/{id}")]
        [HttpGet]
        public Produto Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("v1/produtos")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorProdutoViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = model.Notifications
                };

            var produto = new Produto();
            produto.Titulo = model.Titulo;
            produto.CategoriaId = model.CategoriaId;
            produto.CreateDate = DateTime.Now; // Nunca recebe esta informação
            produto.Descricao = model.Descricao;
            produto.Imagem = model.Imagem;
            produto.LastUpdate = DateTime.Now; // Nunca recebe esta informação
            produto.Preco = model.Preco;
            produto.Quantidade = model.Quantidade;

            _repository.Save(produto);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso!",
                Data = produto
            };
        }

        [Route("v2/produtos")]//Isso é exemplo de versionamento, se fosse lançado por exemplo v2.
        [HttpPost]
        public ResultViewModel Post([FromBody]Produto produto)
        {
            _repository.Save(produto);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso!",
                Data = produto
            };
        }

        [Route("v1/produtos")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorProdutoViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar o produto",
                    Data = model.Notifications
                };

            var produto = _repository.Get(model.Id);
            produto.Titulo = model.Titulo;
            produto.CategoriaId = model.CategoriaId;
            // product.CreateDate = DateTime.Now; // Nunca altera a data de criação
            produto.Descricao = model.Descricao;
            produto.Imagem = model.Imagem;
            produto.LastUpdate = DateTime.Now; // Nunca recebe esta informação
            produto.Preco = model.Preco;
            produto.Quantidade = model.Quantidade;

            _repository.Update(produto);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = produto
            };
        }
    }
}