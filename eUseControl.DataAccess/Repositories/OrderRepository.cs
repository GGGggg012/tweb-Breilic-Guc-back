using System.Collections.Generic;
using System.Linq;
using eUseControl.DataAccess.Context;
using eUseControl.Domain.Entities;

namespace eUseControl.DataAccess.Repositories
{
    public class OrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetByUserId(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
