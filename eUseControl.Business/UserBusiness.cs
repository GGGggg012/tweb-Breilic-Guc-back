using System.Security.Claims;
using eUseControl.DataAccess.Repositories;
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
            var users = _repo.GetAll().Where(u => u.IsActive).ToList();
            var result = new List<UserView>();
            foreach (var u in users)
                result.Add(MapToView(u));
            return result;
        }

        public UserView GetById(int id)
        {
            var user = _repo.GetById(id);
            if (user == null || !user.IsActive) return null;
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

        public void ChangePassword(ClaimsPrincipal principal, ChangePasswordRequest req)
        {
            var userId = ExtractUserId(principal);
            var user = _repo.GetById(userId);
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

        private int ExtractUserId(ClaimsPrincipal principal)
        {
            var value = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(value))
                throw new System.UnauthorizedAccessException("User identity not found in token");
            return int.Parse(value);
        }

        private UserView MapToView(Domain.Entities.UserData u)
        {
            return new UserView
            {
                Id = u.Id,
                FirstName = u.FirstName,
                Username = u.Username,
                Email = u.Email,
                Phone = u.Phone,
                Role = u.Role,
                IsActive = u.IsActive,
                RegisteredOn = u.RegisteredOn
            };
        }
    }
}
