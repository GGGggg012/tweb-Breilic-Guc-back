using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using eUseControl.DataAccess.Repositories;
using eUseControl.Model;

namespace eUseControl.Business
{
    public class UserBusiness
    {
        private readonly UserRepository _repo;
        private readonly IMapper _mapper;

        public UserBusiness(UserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public List<UserView> GetAll()
        {
            var users = _repo.GetAll().Where(u => u.IsActive).ToList();
            return _mapper.Map<List<UserView>>(users);
        }

        public UserView GetById(int id)
        {
            var user = _repo.GetById(id);
            if (user == null || !user.IsActive) return null;
            return _mapper.Map<UserView>(user);
        }

        public UserView Update(int id, RegisterRequest req)
        {
            var user = _repo.GetById(id);
            if (user == null) return null;

            user.FirstName = req.FirstName;
            user.Username = req.Username;
            user.Phone = req.Phone;
            _repo.Update(user);
            return _mapper.Map<UserView>(user);
        }

        public void ChangePassword(int id, ChangePasswordRequest req)
        {
            var user = _repo.GetById(id);
            if (user == null)
                throw new System.InvalidOperationException("User not found");

            if (!user.CheckPassword(req.OldPassword))
                throw new System.UnauthorizedAccessException("Wrong current password");

            user.SetPasswordHash(req.NewPassword);
            _repo.Update(user);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
