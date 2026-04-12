using System.Collections.Generic;
using eUseControl.DataAccess.Repositories;
using eUseControl.Domain.Entities;
using eUseControl.Model;

namespace eUseControl.Business
{
    public class ProductBusiness
    {
        private readonly ProductRepository _repo;

        public ProductBusiness(ProductRepository repo)
        {
            _repo = repo;
        }

        public List<ProductView> GetAll()
        {
            var products = _repo.GetAll();
            var result = new List<ProductView>();
            foreach (var p in products)
            {
                result.Add(MapToView(p));
            }
            return result;
        }

        public ProductView GetById(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return null;
            return MapToView(p);
        }

        public ProductView Create(ProductRequest req)
        {
            var product = new Product
            {
                Name = req.Name,
                Description = req.Description,
                Price = req.Price,
                Stock = req.Stock
            };
            _repo.Add(product);
            return MapToView(product);
        }

        public ProductView Update(int id, ProductRequest req)
        {
            var product = _repo.GetById(id);
            if (product == null) return null;

            product.Name = req.Name;
            product.Description = req.Description;
            product.Price = req.Price;
            product.Stock = req.Stock;
            _repo.Update(product);
            return MapToView(product);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        private ProductView MapToView(Product p)
        {
            return new ProductView
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                InStock = p.IsAvailable()
            };
        }
    }
}
