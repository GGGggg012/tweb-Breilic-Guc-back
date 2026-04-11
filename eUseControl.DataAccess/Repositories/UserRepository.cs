using System.Collections.Generic;
using System.Linq;
using eUseControl.DataAccess.Context;
using eUseControl.Domain.Entities;

namespace eUseControl.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<UserData> GetAll()
        {
            return _context.Users.ToList();
        }

        public UserData GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public UserData GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void Add(UserData user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(UserData user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
