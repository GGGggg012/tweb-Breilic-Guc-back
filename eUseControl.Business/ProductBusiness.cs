using AutoMapper;
using System.Collections.Generic;
using eUseControl.DataAccess.Repositories;
using eUseControl.Domain.Entities;
using eUseControl.Model;

namespace eUseControl.Business
{
    public class ProductBusiness
    {
        private readonly ProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductBusiness(ProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public List<ProductView> GetAll()
        {
            var products = _repo.GetAll();
            return _mapper.Map<List<ProductView>>(products);
        }

        public ProductView GetById(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return null;
            return _mapper.Map<ProductView>(p);
        }

        public ProductView Create(ProductRequest req)
        {
            var product = _mapper.Map<Product>(req);
            _repo.Add(product);
            return _mapper.Map<ProductView>(product);
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
            return _mapper.Map<ProductView>(product);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
