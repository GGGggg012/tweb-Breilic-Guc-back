using System;
using System.Collections.Generic;
using System.Security.Claims;
using eUseControl.DataAccess.Repositories;
using eUseControl.Domain.Entities;
using eUseControl.Model;

namespace eUseControl.Business
{
    public class OrderBusiness
    {
        private readonly OrderRepository _orderRepo;
        private readonly ProductRepository _productRepo;

        public OrderBusiness(OrderRepository orderRepo, ProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public List<OrderView> GetAll()
        {
            var orders = _orderRepo.GetAll();
            var result = new List<OrderView>();
            foreach (var o in orders)
                result.Add(MapToView(o));
            return result;
        }

        public List<OrderView> GetByUser(ClaimsPrincipal principal)
        {
            var userId = ExtractUserId(principal);
            var orders = _orderRepo.GetByUserId(userId);
            var result = new List<OrderView>();
            foreach (var o in orders)
                result.Add(MapToView(o));
            return result;
        }

        public OrderView Create(ClaimsPrincipal principal, OrderRequest req)
        {
            var userId = ExtractUserId(principal);

            var product = _productRepo.GetById(req.ProductId);
            if (product == null)
                throw new InvalidOperationException("Product not found");

            product.DecreaseStock(req.Quantity);
            _productRepo.Update(product);

            var order = new Order
            {
                UserId = userId,
                ProductId = req.ProductId,
                Quantity = req.Quantity,
                CreatedAt = DateTime.UtcNow
            };
            order.CalculateTotal(product.Price);

            _orderRepo.Add(order);
            return MapToView(order);
        }

        private int ExtractUserId(ClaimsPrincipal principal)
        {
            var value = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(value))
                throw new UnauthorizedAccessException("User identity not found in token");
            return int.Parse(value);
        }

        private OrderView MapToView(Order o)
        {
            return new OrderView
            {
                Id = o.Id,
                UserId = o.UserId,
                ProductId = o.ProductId,
                Quantity = o.Quantity,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                CreatedAt = o.CreatedAt
            };
        }
    }
}
