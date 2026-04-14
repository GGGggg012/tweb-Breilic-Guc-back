using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eUseControl.DataAccess.Repositories;
using eUseControl.Domain.Entities;
using eUseControl.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eUseControl.Business
{
    public class AuthBusiness
    {
        private readonly UserRepository _repo;
        private readonly IConfiguration _cfg;

        public AuthBusiness(UserRepository repo, IConfiguration cfg)
        {
            _repo = repo;
            _cfg = cfg;
        }

        public string Login(LoginRequest req)
        {
            var user = _repo.GetByEmail(req.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            if (!user.CheckPassword(req.Password))
                throw new UnauthorizedAccessException("Invalid email or password");

            // generate token and return it
            return GenerateJwt(user);
        }

        public void Register(RegisterRequest req)
        {
            if (_repo.ExistsByEmail(req.Email))
                throw new InvalidOperationException("Email already in use");

            var user = new UserData
            {
                FirstName = req.FirstName,
                Username = req.Username,
                Email = req.Email,
                Phone = req.Phone,
                RegisteredOn = DateTime.UtcNow
            };
            user.SetPasswordHash(req.Password);
            _repo.Add(user);
        }

        private string GenerateJwt(UserData user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
