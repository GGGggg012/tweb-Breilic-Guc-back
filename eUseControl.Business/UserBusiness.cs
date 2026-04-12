using System.Collections.Generic;
using eUseControl.DataAccess.Repositories;
using eUseControl.Domain.Entities;
using eUseControl.Model;

namespace eUseControl.Business
{
    public class UserBusiness
    {
        private readonly UserRepository _repo;

        public UserBusiness(UserRepository repo)
        {
            _repo = repo;
        }

        public List<UserView> GetAll()
        {
            var users = _repo.GetAll();
            var result = new List<UserView>();
            foreach (var u in users)
            {
                result.Add(MapToView(u));
            }
            return result;
        }

        public UserView GetById(int id)
        {
            var user = _repo.GetById(id);
            if (user == null) return null;
            return MapToView(user);
        }

        public UserView Update(int id, RegisterRequest req)
        {
            var user = _repo.GetById(id);
            if (user == null) return null;

            user.FirstName = req.FirstName;
            user.Username = req.Username;
            user.Phone = req.Phone;
            _repo.Update(user);
            return MapToView(user);
        }

        private UserView MapToView(UserData u)
        {
            return new UserView
            {
                Id = u.Id,
                FirstName = u.FirstName,
                Username = u.Username,
                Email = u.Email,
                Phone = u.Phone,
                Role = u.Role,
                RegisteredOn = u.RegisteredOn
            };
        }
    }
}
